using R3;

namespace Interface.InGame.Stage
{
    public interface IGoalEventView
    {
        public Observable<Unit> Performed { get; }
    }

    public interface ICameraView
    {
        public void SetOrthoSize(float orthoSize);
    }

    /// <summary>
    /// ピンチイン・ピンチアウトの入力を受け取る
    /// </summary>
    public interface IPinchView
    {
        public float Pool();
    }
}