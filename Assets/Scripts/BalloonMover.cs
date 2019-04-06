using UnityEngine;

namespace Klyukay.Balloons
{
    
    public class BalloonMover : MonoBehaviour
    {
        
        private Transform _cTransform;
        
        private float _releaseY;
        private float _moveSpeed;

        public Balloon Balloon { private get; set; }

        private void Awake()
        {
            _cTransform = transform;
        }

        private void Update()
        {
            var p = _cTransform.localPosition;
            p.y += Time.deltaTime * _moveSpeed;
            _cTransform.localPosition = p;
            
            // ReSharper disable once UseNullPropagation
            if (p.y >= _releaseY && !ReferenceEquals(Balloon, null)) Balloon.OnFlewAway();
        }

        public void Prepare(float speed, float diameter, Vector4 fieldRect)
        {
            _moveSpeed = speed;

            var t = _cTransform;
            var radius = diameter / 2f;
            
            // Расчет допустимых границ по x, в которых шар может находиться
            var widthRange = new Vector2(fieldRect.x + radius,fieldRect.z - radius);
            // Расчет точки откуда шар стартует
            var startY = fieldRect.y - radius;
            // Расчет точки где шар должен исчезнуть
            _releaseY = fieldRect.w + radius;
            
            // Выставление стартовой позиции
            t.localPosition = new Vector3(Random.Range(widthRange.x, widthRange.y), startY, 0);
        }
        
    }
    
}