using System;
using System.Collections;
using UnityEngine;

namespace Klyukay.BalloonsGame
{

    /// <summary>
    /// Подсчитывает очки, следит за состоянием игры, подсчитывает время.  
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        
        #region Singleton
        
        //Singleton - зло
        public static GameManager Instance { get; private set; }
        
        #endregion Singleton

        [SerializeField] private GameSettings settings;

        [SerializeField] private CameraSetup cameraSetup;
        [SerializeField] private BalloonsProcessor generator;

        private GameState _state = GameState.NotStarted;
        
        /// <summary>
        /// Состояние игры: игра не запущена, запущена и на паузе.
        /// </summary>
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
        
        /// <summary>
        /// Время до конца игры.
        /// </summary>
        public float GameTimer { get; private set; }
        
        /// <summary>
        /// Скорость игры, в данный момент используется только для запуска шариков с нужной скоростью
        /// </summary>
        public float GameSpeed => settings.AccelerationCurve.Evaluate(1f - GameTimer / settings.SessionTime);
        
        /// <summary>
        /// Настройки игры
        /// </summary>
        public GameSettings Settings => settings;

        private int _points;
        /// <summary>
        /// Набранное количество очков.
        /// </summary>
        public int Points
        {
            get => _points;
            private set
            {
                value = Mathf.Max(value, 0);
                if (_points == value) return;
                
                _points = value;
                PointsChanged?.Invoke(Points);
            }
        }

        /// <summary>
        /// Событие, вызываемое при смене состояния игры.
        /// </summary>
        public event Action<GameState> GameStateChanged;
        
        /// <summary>
        /// Событие, вызываемое при обновлении количества очков.
        /// </summary>
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

        /// <summary>
        /// Запускаем игру.
        /// </summary>
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

        /// <summary>
        /// Ставим игру на паузу.
        /// </summary>
        public void PauseGame()
        {
            if (State != GameState.Started) return;

            Time.timeScale = 0f;
            State = GameState.Paused;
        }
        
        /// <summary>
        /// Перезапускаем игру.
        /// </summary>
        public void RestartGame()
        {
            if (State != GameState.Paused) return;
            StartGame();
        }

        /// <summary>
        /// Снимаем игру с паузы.
        /// </summary>
        public void ContinueGame()
        {
            if (State != GameState.Paused) return;

            State = GameState.Started;
            Time.timeScale = 1f;
        }
        
        /// <summary>
        /// Останавливаем игру.
        /// </summary>
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

        public void AddPoints(int points)
        {
            if (State == GameState.Started) Points += points;   
        }

    }

}