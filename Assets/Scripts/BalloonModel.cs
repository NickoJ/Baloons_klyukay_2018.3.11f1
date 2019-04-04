using UnityEngine;

namespace Klyukay.Balloons
{
    
    [System.Serializable]
    public struct BalloonModel
    {
        
        [SerializeField] private float size;
        [SerializeField] private float score;
        [SerializeField] private InRangeFloat speed;

        public float Size => size;
        public float Points => score;
        public InRangeFloat Speed => speed;

    }
    
}