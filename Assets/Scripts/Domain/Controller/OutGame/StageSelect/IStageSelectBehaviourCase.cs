using System.Collections.Generic;
using Module.StateMachine;
using Structure.OutGame;
using Structure.Util;

namespace Domain.Controller.OutGame.StageSelect
{
    public class StageSelectStateMachine : StateMachineBase<StageSelectStateType>
    {
        public StageSelectStateMachine
        (
            IState<StageSelectStateType> state,
            IReadOnlyList<IStateBehaviour<StageSelectStateType>> behaviourEntities
        ) : base(state, behaviourEntities)
        {
        }
    }

    public abstract class StageSelectStateBehaviourBase : StateBehaviour<StageSelectStateType>
    {
        protected StageSelectStateBehaviourBase
        (
            StageSelectStateType stateMask,
            IMutStateEntity<StageSelectStateType> stateEntity
        ) : base(stateMask, stateEntity)
        {
        }
    }
}