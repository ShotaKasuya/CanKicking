namespace Domain.IRepository.OutGame
{
    public interface ISelectedStageRepository
    {
        public void SetSelectedStage(string stageName);
        public string SelectedStage { get; }
    }
}