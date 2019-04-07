using System;
using System.Collections;
using UnityEngine;

namespace Klyukay.Balloons
{

    public class GameManager : MonoBehaviour
    {
        
        #region Singleton
        
        public static GameManager Instance { get; private set; }
        //P.S. Singleton - зло
        
        #endregion Singleton

        [SerializeField] private GameSettings settings;

        [SerializeField] private CameraSetup cameraSetup;
        [SerializeField] private BalloonGenerator generator;

        private GameState _state = GameState.NotStarted;
        public GameState State
        {
            get => _state;
            private set
            {
                if (_state == value) return;
                _state = value;
                GameStateChanged?.Invoke(_state);
            }
        }
        
        public float GameTimer { get; private set; }
        public float GameSpeed => settings.AccelerationCurve.Evaluate(1f - GameTimer / settings.SessionTime);
        public GameSettings Settings => settings;

        private int _points;
        public int Points
        {
            get => _points;
            set
            {
                value = Mathf.Max(value, 0);
                if (_points == value) return;
                
                _points = value;
                PointsChanged?.Invoke(Points);
            }
        }

        public event Action<GameState> GameStateChanged;
        public event Action<int> PointsChanged; 
        
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
            cameraSetup.Initialize();
            generator.Initialize();

            StartGame();
        }

        private void OnDestroy()
        {
            GameStateChanged = null;
        }

        public void StartGame()
        {
            if (State == GameState.Started) return;

            Points = 0;
            GameTimer = settings.SessionTime;
            generator.BeginGeneratingBalloons();

            State = GameState.Started;
            Time.timeScale = 1f;
            
            StartCoroutine(GameSessionTick());
        }

        public void PauseGame()
        {
            if (State != GameState.Started) return;

            Time.timeScale = 0f;
            State = GameState.Paused;
        }
        
        public void RestartGame()
        {
            if (State != GameState.Paused) return;
            StartGame();
        }

        public void ContinueGame()
        {
            if (State != GameState.Paused) return;

            State = GameState.Started;
            Time.timeScale = 1f;
        }
        
        private void StopGame()
        {
            if (State == GameState.NotStarted) return;
            generator.StopGeneratingBalloons();

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

        public void AddPoints(int points) => Points += points;

    }

}