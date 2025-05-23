using System.Collections.Generic;
using Module.StateMachine;
using Structure.OutGame;
using UnityEngine;
using VContainer.Unity;

namespace Domain.UseCase.OutGame.StageSelect
{
    public class StageSelectStateMachine : AbstractStateMachine<StageSelectStateType>, ITickable
    {
        public StageSelectStateMachine
        (
            IState<StageSelectStateType> state,
            IReadOnlyList<IStateBehaviour<StageSelectStateType>> behaviourEntities
        ) : base(state, behaviourEntities)
        {
        }


        public void Tick()
        {
            OnTick(Time.deltaTime);
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
        public StageSelectState
        (
        ) : base(StageSelectStateType.None)
        {
        }
    }
}