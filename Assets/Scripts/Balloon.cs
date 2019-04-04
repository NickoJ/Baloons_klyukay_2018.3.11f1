using UnityEngine;

namespace Klyukay.Balloons
{
    
    [RequireComponent(typeof(SpriteRenderer))]
    public class Balloon : MonoBehaviour
    {

        private SpriteRenderer _spriteRender;

        private void Awake()
        {
            _spriteRender = GetComponent<SpriteRenderer>();
        }

        public void Prepare(Color color)
        {
            _spriteRender.color = color;
        }
    }
    
}