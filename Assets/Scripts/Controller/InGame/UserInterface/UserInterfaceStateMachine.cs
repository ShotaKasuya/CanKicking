using System.Collections.Generic;
using Module.StateMachine;
using Structure.InGame.UserInterface;
using Structure.Utility;

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

    public class UserInterfaceState : AbstractStateType<UserInterfaceStateType>
    {
        public UserInterfaceState() : base(UserInterfaceStateType.Normal)
        {
        }
    }
}