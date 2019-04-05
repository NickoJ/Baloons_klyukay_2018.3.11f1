using System.Collections;
using UnityEngine;

namespace Klyukay.Balloons
{

    public class GameManager : MonoBehaviour
    {

        [SerializeField] private GameSettings settings;

        [SerializeField] private CameraSetup cameraSetup;
        [SerializeField] private BalloonGenerator generator;

        private float _gameTimer;
        
        private void Start()
        {
            cameraSetup.Initialize(settings);
            generator.Initialize(settings);

            StartGame();
        }

        private void StartGame()
        {
            _gameTimer = settings.SessionTime;
            generator.BeginGeneratingBalloons();
            StartCoroutine(GameSessionTick());
        }

        private void StopGame()
        {
            generator.StopGeneratingBallons();
        }

        private IEnumerator GameSessionTick()
        {
            while (_gameTimer > 0)
            {
                yield return null;
                _gameTimer -= Time.deltaTime;
            }

            StopGame();
        }
        
    }

}