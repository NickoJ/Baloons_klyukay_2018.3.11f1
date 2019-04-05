using UnityEngine;

namespace Klyukay.Balloons
{
    
    [System.Serializable]
    public class BalloonModel
    {
        public static readonly BalloonModel Default = new BalloonModel(1, 1, new RandomRangeFloat(1, 0));  
        
        [SerializeField] private float size;
        [SerializeField] private float score;
        [SerializeField] private RandomRangeFloat speed;

        public float Size => size;
        public float Points => score;
        public RandomRangeFloat Speed => speed;

        public BalloonModel(float size, float score, RandomRangeFloat speed)
        {
            this.size = size;
            this.score = score;
            this.speed = speed;
        }
        
    }
    
}