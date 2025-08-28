using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Interface.Logic.InGame;
using Module.StateMachine;
using R3;
using Structure.InGame.UserInterface;

namespace Controller.InGame.UserInterface
{
    public class UserInterfaceStateMachine : AbstractAsyncStateMachine<UserInterfaceStateType>
    {
        public UserInterfaceStateMachine
        (
            IState<UserInterfaceStateType> state,
            IReadOnlyList<IStateBehaviour<UserInterfaceStateType>> behaviourEntities,
            CompositeDisposable compositeDisposable
        ) : base(state, behaviourEntities, compositeDisposable)
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

        public void Reset()
        {
            ChangeState(EntryState).Forget();
        }
    }
}