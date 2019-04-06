using UnityEngine;
using UnityEngine.UI;

namespace Klyukay.Balloons.Controllers
{
    
    public class RestartPanel : MonoBehaviour
    {

        [SerializeField] private Button button;

        private void Start()
        {
            button.onClick.AddListener(OnRestartRequest);

            var gm = GameManager.Instance;
            gm.GameStateChanged += SetupViewForState;
            SetupViewForState(gm.State);
        }

        private void OnDestroy()
        {
            GameManager.Instance.GameStateChanged -= SetupViewForState;
        }

        private void SetupViewForState(GameManager.GameState state)
        {
            gameObject.SetActive(state == GameManager.GameState.NotStarted);
        }
        
        private static void OnRestartRequest() => GameManager.Instance.StartGame();
        
    }
    
}