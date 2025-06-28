namespace Interface.InGame.Stage
{
    public interface ICameraZoomModel
    {
        public float ZoomLevel { get; }

        public void SetZoomLevel(float level);

        public float ZoomMaxLevel { get; }
        public float ZoomMinLevel { get; }
    }
}