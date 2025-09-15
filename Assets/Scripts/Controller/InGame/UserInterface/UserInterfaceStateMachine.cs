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
            IStateType<UserInterfaceStateType> stateType,
            IReadOnlyList<IAsyncStateBehaviour<UserInterfaceStateType>> behaviourEntities,
            CompositeDisposable compositeDisposable
        ) : base(stateType, behaviourEntities, compositeDisposable)
        {
        }
    }

    public abstract class UserInterfaceBehaviourBase : AbstractAsyncStateBehaviour<UserInterfaceStateType>
    {
        protected UserInterfaceBehaviourBase
        (
            UserInterfaceStateType userInterfaceStateType,
            IMutAsyncStateType<UserInterfaceStateType> innerState
        ) : base(userInterfaceStateType, innerState)
        {
        }
    }

    public class UserInterfaceState : AbstractAsyncStateType<UserInterfaceStateType>, IResetable
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