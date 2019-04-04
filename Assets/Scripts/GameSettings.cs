using UnityEngine;

namespace Klyukay.Balloons
{
    
    [CreateAssetMenu(menuName = "Balloons/Game settings", fileName = "GameSettings")]
    public class GameSettings : ScriptableObject
    {

        [SerializeField] private Balloon balloonPrefab;
        [SerializeField] private InRangeFloat balloonAppearTime = new InRangeFloat(5, 4); 
        [SerializeField] private int poolInitSize = 10;

        public Balloon BalloonPrefab => balloonPrefab;
        public InRangeFloat BalloonAppearTime => balloonAppearTime;
        public int PoolInitSize => poolInitSize;
        
    }
    
}