using UnityEngine;

namespace Klyukay.Balloons
{
    
    [System.Serializable]
    public struct InRangeFloat
    {
        
        [SerializeField] private float value;
        [SerializeField] private float offset;

        public InRangeFloat(int value, int offset)
        {
            this.value = value;
            this.offset = offset;
        }

        public float Next() => value + Random.Range(-offset, offset);
        
    }
    
}