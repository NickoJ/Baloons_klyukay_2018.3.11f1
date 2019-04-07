namespace Klyukay.BalloonsGame
{

    public enum ReleaseKind : byte { FlyAway, Popped }
    
    public readonly struct BalloonReleaseEvent
    {

        public readonly Balloon Balloon;
        public readonly int Points;
        public readonly ReleaseKind Kind;

        public BalloonReleaseEvent(Balloon balloon, int points, ReleaseKind kind)
        {
            Balloon = balloon;
            Points = points;
            Kind = kind;
        }

    }
}