using System;
using UnityEngine;

using Random = UnityEngine.Random;

namespace Klyukay.Balloons
{
    
    [RequireComponent(typeof(SpriteRenderer))]
    [RequireComponent(typeof(BalloonMover))]
//    [RequireComponent(typeof(BalloonTouchDetector))]
    public class Balloon : MonoBehaviour
    {

        private Transform _cTransform;
        private SpriteRenderer _spriteRender;
        private BalloonMover _mover;

        private BalloonModel _model;

        public event Action<BalloonReleaseEvent> Released; 
        
        private void Awake()
        {
            _cTransform = transform;
            _spriteRender = GetComponent<SpriteRenderer>();
            _mover = GetComponent<BalloonMover>();
            _mover.Balloon = this;
        }

        private void OnDestroy()
        {
            Released = null;
        }

        public void Prepare(BalloonModel model, Color color, float gameSpeed, Vector4 fieldRect)
        {
            _model = model;
            _spriteRender.color = color;
            _cTransform.localScale = new Vector3(_model.Size, _model.Size, 1);

            _mover.Prepare(_model.Speed.Next() * gameSpeed, _model.Size, fieldRect);
        }

        public void OnFlewAway() => Released?.Invoke(new BalloonReleaseEvent(this, 0));
        public void OnPop() => Released?.Invoke(new BalloonReleaseEvent(this, _model?.Points ?? 0));
        
        public void Reset()
        {
            Released = null;
            gameObject.SetActive(false);
        }

    }
    
}