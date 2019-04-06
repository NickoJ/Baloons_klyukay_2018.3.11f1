using System;
using System.Collections.ObjectModel;
using UnityEngine;

// ReSharper disable ConvertToAutoProperty

namespace Klyukay.Balloons
{
    
    [CreateAssetMenu(menuName = "Balloons/Game settings", fileName = "GameSettings")]
    public class GameSettings : ScriptableObject
    {

        [SerializeField] private float sessionTime = 60f;
        [SerializeField] private AnimationCurve accelerationCurve;
        
        [SerializeField] private Balloon balloonPrefab;
        [SerializeField] private RandomRangeFloat balloonAppearTime = new RandomRangeFloat(5, 4); 
        [SerializeField] private int poolInitSize = 10;
        
        [SerializeField] private float gameFieldInitWidth;
        
        [SerializeField] private BalloonModel[] balloonModels;
        [SerializeField] private Color[] balloonColors;

        public float SessionTime => sessionTime > 0 ? sessionTime : 1f;
        public AnimationCurve AccelerationCurve => accelerationCurve;

        public Balloon BalloonPrefab => balloonPrefab;
        public RandomRangeFloat BalloonAppearTime => balloonAppearTime;
        public int PoolInitSize => poolInitSize;

        public float GameFieldInitWidth => gameFieldInitWidth;

        public ReadOnlyCollection<BalloonModel> BalloonModels => Array.AsReadOnly(balloonModels);
        public ReadOnlyCollection<Color> BalloonColors => Array.AsReadOnly(balloonColors);
        
        public Vector4 GameFieldRect { get; set; }
    }
    
}