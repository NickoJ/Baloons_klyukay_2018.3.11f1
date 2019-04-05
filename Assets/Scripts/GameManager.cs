using Klyukay.Balloons;
using UnityEngine;

namespace Klyukay.Balloons
{

    public class GameManager : MonoBehaviour
    {

        [SerializeField] private GameSettings settings;

        [SerializeField] private CameraSetup cameraSetup;
        [SerializeField] private BalloonGenerator generator;

        private void Start()
        {
            cameraSetup.Initialize(settings);
            generator.Initialize(settings);

            generator.BeginGeneratingBalloons();
        }
    }

}