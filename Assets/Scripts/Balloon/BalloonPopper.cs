using UnityEngine;
using UnityEngine.EventSystems;

namespace Klyukay.BalloonsGame
{
    
    public class BalloonPopper : MonoBehaviour, IPointerDownHandler
    {
        
        public Balloon Balloon { private get; set; }
        
        public void OnPointerDown(PointerEventData eventData) => Balloon.OnPop();
        
    }
    
}