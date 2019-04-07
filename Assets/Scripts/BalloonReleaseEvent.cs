namespace Klyukay.Balloons
{
    public readonly struct BalloonReleaseEvent
    {

        public readonly Balloon Balloon;
        public readonly int Points;

        public BalloonReleaseEvent(Balloon balloon, int points)
        {
            Balloon = balloon;
            Points = points;
        }

    }
}