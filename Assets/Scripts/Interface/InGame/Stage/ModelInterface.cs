namespace Interface.InGame.Stage
{
    public interface ICameraZoomModel
    {
        public float SetZoomLevel(float level);
        public float GetOrthoSize();
        
        public float ZoomLevel { get; }
        public float Sensitivity { get; }
    }
}