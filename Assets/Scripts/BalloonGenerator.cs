using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Klyukay.Balloons
{

    internal class BalloonGenerator : MonoBehaviour
    {

        private MonoPool<Balloon> _pool;
        private InRangeFloat _appearTime;

        private IList<Color> _colors;
        
        private Coroutine _generateRoutine;
        
        public void Initialize(GameSettings settings)
        {
            _pool = new MonoPool<Balloon>(settings.BalloonPrefab, settings.PoolInitSize);
            _appearTime = settings.BalloonAppearTime;
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
            Balloon balloon = null;
            while (true)
            {
                yield return new WaitForSeconds(_appearTime.Next());
                _pool.Release(balloon);
                balloon = _pool.Take();
                balloon.Prepare(GetRandomBalloonColor());
                balloon.gameObject.SetActive(true);
            }
        }

        private Color GetRandomBalloonColor()
        {
            if (_colors?.Count == 0) return Color.red;
            return _colors[Random.Range(0, _colors.Count)];
        }
        
    }

}