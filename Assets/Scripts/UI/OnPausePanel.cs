using UnityEngine;
using UnityEngine.UI;

namespace Klyukay.Balloons.UI
{

    public class OnPausePanel : MonoBehaviour
    {

        [SerializeField] private Button restartButton;
        [SerializeField] private Button continueButton;
        
        private void Start()
        {
            restartButton.onClick.AddListener(OnRestartRequest);
            continueButton.onClick.AddListener(OnContinueRequest);
            
            var gm = GameManager.Instance;
            gm.GameStateChanged += SetupViewForState;
            SetupViewForState(gm.State);
        }

        private void SetupViewForState(GameManager.GameState state)
        {
            gameObject.SetActive(state == GameManager.GameState.Paused);
        }

        private static void OnRestartRequest() => GameManager.Instance.RestartGame();
        private static void OnContinueRequest() => GameManager.Instance.ContinueGame();

    }

}