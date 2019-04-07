using UnityEngine;
using UnityEngine.UI;

namespace Klyukay.Balloons.UI
{
    
    [RequireComponent(typeof(Text))]
    public class PointsPanel : MonoBehaviour
    {

        private Text _text;
        
        private void Start()
        {
            _text = GetComponent<Text>();

            var game = GameManager.Instance;
            game.PointsChanged += UpdatePoints;
            UpdatePoints(game.Points);
        }

        private void OnDestroy()
        {
            GameManager.Instance.PointsChanged -= UpdatePoints;
        }

        private void UpdatePoints(int points)
        {
            _text.text = points.ToString();
        }
        
    }
    
}