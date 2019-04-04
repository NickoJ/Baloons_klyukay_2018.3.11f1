using UnityEngine;

namespace Klyukay.Balloons
{
    
    [System.Serializable]
    public class BalloonModel
    {
        public static readonly BalloonModel Default = new BalloonModel(1, 1, new InRangeFloat(1, 0));  
        
        [SerializeField] private float size;
        [SerializeField] private float score;
        [SerializeField] private InRangeFloat speed;

        public float Size => size;
        public float Points => score;
        public InRangeFloat Speed => speed;

        public BalloonModel(float size, float score, InRangeFloat speed)
        {
            this.size = size;
            this.score = score;
            this.speed = speed;
        }
        
    }
    
}