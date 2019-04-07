using System.Collections;
using System.Collections.Generic;
using Klyukay.Helpers;
using UnityEngine;

namespace Klyukay.BalloonsGame
{

    internal class BalloonGenerator : MonoBehaviour
    {

        private MonoPool<Balloon> _pool;

        private GameManager _game;
        
        private IList<Color> _colors;
        private IList<BalloonModel> _balloonModels;

        private readonly HashSet<Balloon> _flyingBalloons = new HashSet<Balloon>();
        private Coroutine _generateRoutine;
        
        public void Initialize()
        {
            _game = GameManager.Instance;
            
            var settings = _game.Settings;
            _pool = new MonoPool<Balloon>(settings.BalloonPrefab, settings.PoolInitSize);
            
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
            var settings = _game.Settings;
            
            while (true)
            {
                yield return new WaitForSeconds(settings.BalloonAppearTime.Next());
                var balloon = _pool.Take();
                balloon.Released += OnBalloonRelease;
                balloon.Prepare(GetRandomBalloonModel(), GetRandomBalloonColor(), _game.GameSpeed, settings.GameFieldRect);
                _flyingBalloons.Add(balloon);
                balloon.gameObject.SetActive(true);
            }
        }

        private void OnBalloonRelease(BalloonReleaseEvent e)
        {
            _game.AddPoints(e.Points);
            
            e.Balloon.Reset();
            _flyingBalloons.Remove(e.Balloon);
            _pool.Release(e.Balloon);
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