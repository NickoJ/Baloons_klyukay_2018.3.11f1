using Klyukay.Helpers;
using UnityEngine;
using UnityEngine.Serialization;


namespace Klyukay.BalloonsGame
{
    
    [System.Serializable]
    public class BalloonModel
    {
        public static readonly BalloonModel Default = new BalloonModel(1, 1, new RandomRangeFloat(1, 0));  
        
        [SerializeField] private float size;
        [FormerlySerializedAs("score")] [SerializeField] private int points;
        [SerializeField] private RandomRangeFloat speed;

        // ReSharper disable ConvertToAutoPropertyWithPrivateSetter
        public float Size => size;
        public int Points => points;
        public RandomRangeFloat Speed => speed;
        // ReSharper restore ConvertToAutoPropertyWithPrivateSetter

        public BalloonModel(float size, int points, RandomRangeFloat speed)
        {
            this.size = size;
            this.points = points;
            this.speed = speed;
        }
        
    }
    
}