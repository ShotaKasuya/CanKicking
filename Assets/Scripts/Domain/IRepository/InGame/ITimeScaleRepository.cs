namespace Domain.IRepository.InGame
{
    public interface ITimeScaleRepository
    {
        public const float Normal = 1;
        public const float Stop = 0;
        public float FryState { get; }
    }

    public interface IMutTimeScaleRepository
    {
        public void SetFryStateTimeScale(float timeScale);
    }
}