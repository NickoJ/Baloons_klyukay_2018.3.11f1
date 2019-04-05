using System;
using System.Collections;
using UnityEngine;

namespace Klyukay.Balloons
{

    public class GameManager : MonoBehaviour
    {
        
        #region Singleton
        
        //P.S. Singleton это зло, но в тестовом задании про шарики подтягивать DI контейнер это сурово.
        public static GameManager Instance { get; private set; }
        
        #endregion Singleton

        [SerializeField] private GameSettings settings;

        [SerializeField] private CameraSetup cameraSetup;
        [SerializeField] private BalloonGenerator generator;

        private GameState _state = GameState.NotStarted;
        public GameState State
        {
            get => _state;
            set
            {
                if (_state == value) return;
                _state = value;
                GameStateChanged?.Invoke(_state);
            }
        }
        
        public float GameTimer { get; private set; }

        public GameSettings Settings => settings;

        public event Action<GameState> GameStateChanged; 

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(this);
                return;
            }
            Instance = this;
        }

        private void Start()
        {
            cameraSetup.Initialize(settings);
            generator.Initialize(settings);

            StartGame();
        }

        private void OnDestroy()
        {
            GameStateChanged = null;
        }

        private void StartGame()
        {
            if (State == GameState.Started) return;
            
            GameTimer = settings.SessionTime;
            generator.BeginGeneratingBalloons();

            State = GameState.Started;
            
            StartCoroutine(GameSessionTick());
        }

        private void StopGame()
        {
            if (State == GameState.NotStarted) return;
            generator.StopGeneratingBallons();

            State = GameState.NotStarted;
        }

        private IEnumerator GameSessionTick()
        {
            while (GameTimer > 0)
            {
                yield return null;
                GameTimer -= Time.deltaTime;
            }
            
            GameTimer = 0f;
            StopGame();
        }
        
        public enum GameState : byte
        {
            NotStarted,
            Started,
            Paused
        }
        
    }

}