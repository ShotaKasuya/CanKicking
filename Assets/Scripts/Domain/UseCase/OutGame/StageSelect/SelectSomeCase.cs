using Domain.IUseCase.OutGame;
using Module.StateMachine;
using Structure.OutGame;

namespace Domain.UseCase.OutGame.StageSelect
{
    public class SelectSomeCase : StageSelectStateBehaviourBase
    {
        public SelectSomeCase
        (
            IMutStateEntity<StageSelectStateType> stateEntity
        ) : base(StageSelectStateType.Some, stateEntity)
        {
        }
    }
}