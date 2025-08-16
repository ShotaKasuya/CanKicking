using System.Collections.Generic;
using Interface.InGame.Primary;
using Module.StateMachine;
using ModuleExtension.StateMachine;
using Structure.InGame.UserInterface;

namespace Controller.InGame.UserInterface
{
    public class UserInterfaceStateMachine : StateMachineBase<UserInterfaceStateType>
    {
        public UserInterfaceStateMachine
        (
            IState<UserInterfaceStateType> state,
            IReadOnlyList<IStateBehaviour<UserInterfaceStateType>> behaviourEntities
        ) : base(state, behaviourEntities)
        {
        }
    }

    public abstract class UserInterfaceBehaviourBase : StateBehaviour<UserInterfaceStateType>
    {
        protected UserInterfaceBehaviourBase
        (
            UserInterfaceStateType userInterfaceStateType,
            IMutStateEntity<UserInterfaceStateType> stateEntity
        ) : base(userInterfaceStateType, stateEntity)
        {
        }
    }

    public class UserInterfaceState : AbstractStateType<UserInterfaceStateType>, IResetable
    {
        public UserInterfaceState() : base(UserInterfaceStateType.Normal)
        {
        }

        private const UserInterfaceStateType EntryState = UserInterfaceStateType.Normal;

        public void Reset()
        {
            ChangeState(EntryState);
        }
    }
}