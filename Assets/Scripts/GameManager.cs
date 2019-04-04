using Klyukay.Balloons;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    [SerializeField] private GameSettings settings;
    
    [SerializeField] private BalloonGenerator generator;

    private void Start()
    {
        generator.Initialize(settings);
        
        generator.BeginGeneratingBalloons();
    }
}
