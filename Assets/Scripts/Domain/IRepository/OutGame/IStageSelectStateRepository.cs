using Module.StateMachine;
using Structure.OutGame;

namespace Domain.IRepository.OutGame
{
    public interface IStageSelectStateRepository : IState<StageSelectStateType>
    {
    }

    public interface IMutStageSelectStateRepository : IMutStateEntity<StageSelectStateType>
    {
    }
}