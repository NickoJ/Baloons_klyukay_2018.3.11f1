using UnityEngine;
using UnityEngine.Serialization;
// ReSharper disable ConvertToAutoPropertyWithPrivateSetter

namespace Klyukay.Balloons
{
    
    [System.Serializable]
    public class BalloonModel
    {
        public static readonly BalloonModel Default = new BalloonModel(1, 1, new RandomRangeFloat(1, 0));  
        
        [SerializeField] private float size;
        [FormerlySerializedAs("score")] [SerializeField] private int points;
        [SerializeField] private RandomRangeFloat speed;

        public float Size => size;
        public int Points => points;
        public RandomRangeFloat Speed => speed;

        public BalloonModel(float size, int points, RandomRangeFloat speed)
        {
            this.size = size;
            this.points = points;
            this.speed = speed;
        }
        
    }
    
}