using System;
using UnityEngine;
using Random = UnityEngine.Random;

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

        public event Action<Balloon> FlewAway; 
        
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
            
            if (p.y >= _releaseY) FlewAway?.Invoke(this);
        }

        private void OnDestroy()
        {
            FlewAway = null;
        }

        public void Prepare(BalloonModel model, Color color, float gameSpeed, Vector2 fieldSize)
        {
            _model = model;
            _moveSpeed = _model.Speed.Next() * gameSpeed;
            
            _spriteRender.color = color;
            var t = _cachedTransform;
            
            // Выставляем размеры шару
            t.localScale = new Vector3(_model.Size, _model.Size, 1);
            // Расчет допустимых границ по x, в которых шар может находиться
            var widthRange = new Vector2((-fieldSize.x + _model.Size) / 2,(fieldSize.x - model.Size) / 2);
            // Расчет точки откуда шар стартует
            var startY = (-fieldSize.y - _model.Size) / 2f;
            // Расчет точки где шар должен исчезнуть
            _releaseY = (fieldSize.y + _model.Size) / 2f;
            
            // Выставление стартовой позиции
            t.localPosition = new Vector3(Random.Range(widthRange.x, widthRange.y), startY, 0);
        }
        
        public void Reset()
        {
            FlewAway = null;
            gameObject.SetActive(false);
        }
        
    }
    
}