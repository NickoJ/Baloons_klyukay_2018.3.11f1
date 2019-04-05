using UnityEngine;

namespace Klyukay.Balloons
{

    [RequireComponent(typeof(Camera))]
    public class CameraSetup : MonoBehaviour
    {

        private GameSettings _settings;
        private Camera _camera;
        
        public void Initialize(GameSettings settings)
        {
            _settings = settings;
        }

        private void Awake()
        {
            _camera = GetComponent<Camera>();
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
            _settings.GameFieldSize = new Vector2(w, h);
            _camera.orthographicSize = h / 2f;
        }
        
    }

}