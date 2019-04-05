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
        
        [SerializeField] private Balloon balloonPrefab;
        [SerializeField] private InRangeFloat balloonAppearTime = new InRangeFloat(5, 4); 
        [SerializeField] private int poolInitSize = 10;
        
        [SerializeField] private float gameFieldInitWidth;
        
        [SerializeField] private BalloonModel[] balloonModels;
        [SerializeField] private Color[] balloonColors;

        public float SessionTime => sessionTime;
        
        public Balloon BalloonPrefab => balloonPrefab;
        public InRangeFloat BalloonAppearTime => balloonAppearTime;
        public int PoolInitSize => poolInitSize;

        public float GameFieldInitWidth => gameFieldInitWidth;

        public ReadOnlyCollection<BalloonModel> BalloonModels => Array.AsReadOnly(balloonModels);
        public ReadOnlyCollection<Color> BalloonColors => Array.AsReadOnly(balloonColors);
        
        public Vector2 GameFieldSize { get; set; }
    }
    
}