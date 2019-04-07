using UnityEngine;
using UnityEngine.UI;

namespace Klyukay.BalloonsGame.UI
{

    public class Timer : MonoBehaviour
    {

        [SerializeField] private Slider timerSlider;
        [SerializeField] private Text timerText;
        
        private void Start()
        {
            var gm = GameManager.Instance;
            
            var settings = gm.Settings;
            timerSlider.minValue = 0;
            timerSlider.maxValue = settings.SessionTime;

            gm.GameStateChanged += SetupViewsForState;
            SetupViewsForState(gm.State);
        }

        private void OnDestroy()
        {
            GameManager.Instance.GameStateChanged -= SetupViewsForState;
        }

        private void Update()
        {
            var gm = GameManager.Instance;
            timerSlider.value = gm.GameTimer;
            timerText.text = gm.GameTimer.ToString("00.00");
        }

        private void SetupViewsForState(GameManager.GameState state)
        {
            switch (state)
            {
                case GameManager.GameState.NotStarted:
                    timerSlider.value = timerSlider.minValue;
                    timerText.text = string.Empty;
                    enabled = false;
                    break;
                case GameManager.GameState.Started:
                    enabled = true;
                    Update();
                    break;
                case GameManager.GameState.Paused:
                    enabled = false;
                    break;
            }
        }
        
    }

}