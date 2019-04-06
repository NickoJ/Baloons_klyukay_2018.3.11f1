using UnityEngine;

namespace Klyukay.Balloons
{

    [RequireComponent(typeof(Camera))]
    public class CameraSetup : MonoBehaviour
    {

        private GameSettings _settings;
        private Camera _camera;
        private Transform _cachedTransform;
        
        public void Initialize(GameSettings settings)
        {
            _settings = settings;
        }

        private void Awake()
        {
            _camera = GetComponent<Camera>();
            _cachedTransform = GetComponent<Transform>();
        }

        #if UNITY_EDITOR
        private void Update()
        {
            if (_settings) CalculateField();
        }
        #endif

        private void CalculateField()
        {
            float w = _settings.GameFieldInitWidth;
            float h = w / _camera.aspect;
            
            _camera.orthographicSize = h / 2f;

            var cPos = _cachedTransform.position;
            _settings.GameFieldRect = new Vector4(cPos.x - w / 2f, cPos.y - h / 2,
                cPos.x + w / 2f, cPos.y + h / 2f);
        }
        
    }

}