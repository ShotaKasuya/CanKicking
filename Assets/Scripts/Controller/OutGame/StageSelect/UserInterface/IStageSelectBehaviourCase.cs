using System.Collections.Generic;
using Module.StateMachine;
using Structure.OutGame;
using Structure.Utility;

namespace Controller.OutGame.StageSelect.UserInterface
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

    public class StageSelectState : AbstractStateType<StageSelectStateType>
    {
        public StageSelectState() : base(StageSelectStateType.None)
        {
        }
    }
}