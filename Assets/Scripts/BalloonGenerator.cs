using System;
using System.Collections;
using UnityEngine;

namespace Klyukay.Balloons
{

    internal class BalloonGenerator : MonoBehaviour
    {

        private MonoPool<Balloon> _pool;
        private InRangeFloat _appearTime;

        private Coroutine _generateRoutine;
        
        public void Initialize(GameSettings settings)
        {
            _pool = new MonoPool<Balloon>(settings.BalloonPrefab, settings.PoolInitSize);
            _appearTime = settings.BalloonAppearTime;
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
                yield return new WaitForSeconds(_appearTime.Next());
                Debug.Log($"Generate: {DateTime.Now}");
//                var balloon = _pool.Take();
            }
        }
        
    }

}