using System.Collections.Generic;
using Interface.InGame.Primary;
using Module.StateMachine;
using ModuleExtension.StateMachine;
using Structure.OutGame;

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

    public class StageSelectState : AbstractStateType<StageSelectStateType>, IResetable
    {
        public StageSelectState() : base(EntryState)
        {
        }

        private const StageSelectStateType EntryState = StageSelectStateType.None;

        public void Reset()
        {
            ChangeState(EntryState);
        }
    }
}