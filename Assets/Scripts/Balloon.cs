using UnityEngine;

namespace Klyukay.Balloons
{
    
    [RequireComponent(typeof(SpriteRenderer))]
    public class Balloon : MonoBehaviour
    {

        private SpriteRenderer _spriteRender;

        private BalloonModel _model;
        private float _moveSpeed;
        private float _releaseY;

        private Transform _cachedTransform;

        public bool CanRelease => _cachedTransform.localPosition.y >= _releaseY;
        
        private void Awake()
        {
            _cachedTransform = transform;
            _spriteRender = GetComponent<SpriteRenderer>();
        }

        private void Update()
        {
            var p = _cachedTransform.localPosition;
            p.y += Time.deltaTime * _moveSpeed;
            _cachedTransform.localPosition = p;
        }

        public void Prepare(BalloonModel model, Color color, Vector2 fieldSize)
        {
            _model = model;
            _moveSpeed = _model.Speed.Next();
            
            _spriteRender.color = color;
            var t = _cachedTransform;
            t.localScale = new Vector3(_model.Size, _model.Size, 1);
            var widthRange = new Vector2((-fieldSize.x + _model.Size) / 2,(fieldSize.x - model.Size) / 2);
            var startY = (-fieldSize.y - _model.Size) / 2f;
            t.localPosition = new Vector3(Random.Range(widthRange.x, widthRange.y), startY, 0);
        }
    }
    
}