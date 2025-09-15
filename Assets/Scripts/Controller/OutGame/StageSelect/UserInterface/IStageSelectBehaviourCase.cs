using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Interface.Logic.InGame;
using Module.StateMachine;
using R3;
using Structure.OutGame;

namespace Controller.OutGame.StageSelect.UserInterface
{
    public class StageSelectStateMachine : AbstractAsyncStateMachine<StageSelectStateType>
    {
        public StageSelectStateMachine
        (
            IStateType<StageSelectStateType> stateType,
            IReadOnlyList<IAsyncStateBehaviour<StageSelectStateType>> behaviours,
            CompositeDisposable compositeDisposable
        ) : base(stateType, behaviours, compositeDisposable)
        {
        }
    }

    public abstract class StageSelectStateBehaviourBase : AbstractAsyncStateBehaviour<StageSelectStateType>
    {
        protected StageSelectStateBehaviourBase
        (
            StageSelectStateType stateMask,
            IMutAsyncStateType<StageSelectStateType> innerState
        ) : base(stateMask, innerState)
        {
        }
    }

    public class StageSelectState : AbstractAsyncStateType<StageSelectStateType>, IResetable
    {
        public StageSelectState() : base(StageSelectStateType.None)
        {
        }

        public void Reset()
        {
            ChangeState(EntryState).Forget();
        }
    }
}