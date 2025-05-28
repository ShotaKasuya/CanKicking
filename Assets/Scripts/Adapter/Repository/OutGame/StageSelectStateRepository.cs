using Domain.IRepository.OutGame;
using Module.StateMachine;
using Structure.OutGame;

namespace Adapter.Repository.OutGame
{
    public class StageSelectStateRepository : AbstractStateType<StageSelectStateType>, IStageSelectStateRepository,
        IMutStageSelectStateRepository
    {
        public StageSelectStateRepository
        (
        ) : base(StageSelectStateType.None)
        {
        }
    }
}