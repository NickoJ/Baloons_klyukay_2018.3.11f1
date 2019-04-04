using UnityEngine;

namespace Klyukay.Balloons
{
    
    [RequireComponent(typeof(SpriteRenderer))]
    public class Balloon : MonoBehaviour
    {

        private SpriteRenderer _spriteRender;

        private BalloonModel _model;

        private void Awake()
        {
            _spriteRender = GetComponent<SpriteRenderer>();
        }

        public void Prepare(BalloonModel model, Color color)
        {
            _model = model;
            
            _spriteRender.color = color;
            transform.localScale = new Vector3(_model.Size, _model.Size, 1);
        }
    }
    
}