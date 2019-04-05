using UnityEngine;
using UnityEngine.UI;

namespace Klyukay.Balloons.Controllers
{

    [RequireComponent(typeof(Button))]
    public class PauseButton : MonoBehaviour
    {

        private void Start()
        {
            var button = GetComponent<Button>();
            button.onClick.AddListener(OnClick);

            var gm = GameManager.Instance;
            gm.GameStateChanged += PrepareForState;
            PrepareForState(gm.State);
        }

        private void OnDestroy()
        {
            GameManager.Instance.GameStateChanged -= PrepareForState;
        }

        private void PrepareForState(GameManager.GameState state)
        {
            gameObject.SetActive(state == GameManager.GameState.Started);
        }

        private void OnClick()
        {
            GameManager.Instance.PauseGame();
        }

    }

}