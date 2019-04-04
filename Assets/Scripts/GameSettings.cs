using System;
using System.Collections.ObjectModel;
using UnityEngine;

// ReSharper disable ConvertToAutoProperty

namespace Klyukay.Balloons
{
    
    [CreateAssetMenu(menuName = "Balloons/Game settings", fileName = "GameSettings")]
    public class GameSettings : ScriptableObject
    {

        [SerializeField] private Balloon balloonPrefab;
        [SerializeField] private InRangeFloat balloonAppearTime = new InRangeFloat(5, 4); 
        [SerializeField] private int poolInitSize = 10;
        [SerializeField] private BalloonModel[] balloonModels;
        [SerializeField] private Color[] balloonColors;
        
        public Balloon BalloonPrefab => balloonPrefab;
        public InRangeFloat BalloonAppearTime => balloonAppearTime;
        public int PoolInitSize => poolInitSize;
        public ReadOnlyCollection<BalloonModel> BalloonModels => Array.AsReadOnly(balloonModels);
        public ReadOnlyCollection<Color> BalloonColors => Array.AsReadOnly(balloonColors);
        
    }
    
}