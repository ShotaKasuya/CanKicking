using System.Collections.Generic;
using Module.StateMachine;
using Structure.InGame.UserInterface;
using Structure.Util;

namespace Domain.Controller.InGame
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
}