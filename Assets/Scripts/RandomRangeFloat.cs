using UnityEngine;

namespace Klyukay.Balloons
{
    
    [System.Serializable]
    public struct RandomRangeFloat
    {
        
        [SerializeField] private float value;
        [SerializeField] private float offset;

        public RandomRangeFloat(int value, int offset)
        {
            this.value = value;
            this.offset = offset;
        }

        public float Next() => value + Random.Range(-offset, offset);
        
    }
    
}