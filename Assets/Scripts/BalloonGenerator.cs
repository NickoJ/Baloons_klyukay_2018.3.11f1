using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Klyukay.Balloons
{

    internal class BalloonGenerator : MonoBehaviour
    {

        private MonoPool<Balloon> _pool;

        private GameSettings _settings;
        private IList<Color> _colors;
        private IList<BalloonModel> _balloonModels;

        private readonly HashSet<Balloon> _flyingBalloons = new HashSet<Balloon>();
        private Coroutine _generateRoutine;
        
        public void Initialize(GameSettings settings)
        {
            _pool = new MonoPool<Balloon>(settings.BalloonPrefab, settings.PoolInitSize);
            
            _settings = settings;
            _balloonModels = settings.BalloonModels;
            _colors = settings.BalloonColors;
        }

        public void BeginGeneratingBalloons()
        {
            StopGeneratingBalloons();
            ResetAllFlyingBalloons();
            _generateRoutine = StartCoroutine(GenerateBalloonsAsync());
        }

        public void StopGeneratingBalloons()
        {
            if (_generateRoutine == null) return;
            
            StopCoroutine(_generateRoutine);
            _generateRoutine = null;
        }

        private void ResetAllFlyingBalloons()
        {
            foreach (var balloon in _flyingBalloons) _pool.Release(balloon);
            
            _flyingBalloons.Clear();
        }

        private IEnumerator GenerateBalloonsAsync()
        {
            var gm = GameManager.Instance;
            while (true)
            {
                yield return new WaitForSeconds(_settings.BalloonAppearTime.Next());
                var balloon = _pool.Take();
                balloon.FlewAway += BalloonOnFlewAway;
                balloon.Prepare(GetRandomBalloonModel(), GetRandomBalloonColor(), gm.GameSpeed, _settings.GameFieldRect);
                _flyingBalloons.Add(balloon);
                balloon.gameObject.SetActive(true);
            }
        }

        private void BalloonOnFlewAway(Balloon b)
        {
            b.Reset();
            _flyingBalloons.Remove(b);
            _pool.Release(b);
        }

        private BalloonModel GetRandomBalloonModel()
        {
            if (_balloonModels == null || _balloonModels.Count == 0) return BalloonModel.Default;
            return _balloonModels[Random.Range(0, _balloonModels.Count)];
        }
        
        private Color GetRandomBalloonColor()
        {
            if (_colors == null || _colors.Count == 0) return Color.red;
            return _colors[Random.Range(0, _colors.Count)];
        }
        
    }

}