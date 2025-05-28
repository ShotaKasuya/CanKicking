using System.Collections.Generic;
using Module.StateMachine;
using Structure.InGame.UserInterface;
using UnityEngine;
using VContainer.Unity;

namespace Domain.Controller.InGame
{
    public class UserInterfaceStateMachine : AbstractStateMachine<UserInterfaceStateType>, ITickable
    {
        public UserInterfaceStateMachine
        (
            IState<UserInterfaceStateType> state,
            IReadOnlyList<IStateBehaviour<UserInterfaceStateType>> behaviourEntities
        ) : base(state, behaviourEntities)
        {
        }

        public void Tick()
        {
            OnTick(Time.deltaTime);
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