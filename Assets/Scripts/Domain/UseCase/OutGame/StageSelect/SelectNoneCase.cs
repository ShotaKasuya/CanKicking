using Domain.IUseCase.OutGame;
using Module.StateMachine;
using Structure.OutGame;

namespace Domain.UseCase.OutGame.StageSelect
{
    public class SelectNoneCase : StageSelectStateBehaviourBase

    {
        public SelectNoneCase
        (
            IMutStateEntity<StageSelectStateType> stateEntity
        ) : base(StageSelectStateType.None, stateEntity)
        {
        }
        
        
    }
}