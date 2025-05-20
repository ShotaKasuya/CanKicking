using Domain.IRepository.OutGame;

namespace Adapter.Repository.OutGame
{
    public class SelectedStageRepository: ISelectedStageRepository
    {
        public void SetSelectedStage(string stageName)
        {
            SelectedStage = stageName;
        }

        public string SelectedStage { get; private set; }
    }
}