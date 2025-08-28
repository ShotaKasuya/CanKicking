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
            IState<StageSelectStateType> state,
            IReadOnlyList<IStateBehaviour<StageSelectStateType>> behaviourEntities,
            CompositeDisposable compositeDisposable
        ) : base(state, behaviourEntities, compositeDisposable)
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
        public StageSelectState() : base(StageSelectStateType.None)
        {
        }

        public void Reset()
        {
            ChangeState(EntryState).Forget();
        }
    }
}