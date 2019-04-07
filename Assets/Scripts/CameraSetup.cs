using UnityEngine;

namespace Klyukay.BalloonsGame
{

    [RequireComponent(typeof(Camera))]
    public class CameraSetup : MonoBehaviour
    {

        private GameSettings _settings;
        private Camera _camera;
        private Transform _cachedTransform;
        
        public void Initialize()
        {
            _settings = GameManager.Instance.Settings;
        }

        private void Start()
        {
            _camera = GetComponent<Camera>();
            _cachedTransform = GetComponent<Transform>();
            
            CalculateField();
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