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
            _generateRoutine = StartCoroutine(GenerateBalloonsAsync());
        }

        public void StopGeneratingBallons()
        {
            if (_generateRoutine == null) return;
            
            StopCoroutine(_generateRoutine);
            _generateRoutine = null;
        }

        private IEnumerator GenerateBalloonsAsync()
        {
            while (true)
            {
                yield return new WaitForSeconds(_settings.BalloonAppearTime.Next());
                var balloon = _pool.Take();
                balloon.FlewAway += BalloonOnFlewAway;
                balloon.Prepare(GetRandomBalloonModel(), GetRandomBalloonColor(), _settings.GameFieldSize);
                balloon.gameObject.SetActive(true);
            }
        }

        private void BalloonOnFlewAway(Balloon b)
        {
            b.Reset();
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