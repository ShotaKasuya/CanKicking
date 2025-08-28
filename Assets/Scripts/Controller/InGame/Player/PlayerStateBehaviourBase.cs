using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Interface.Logic.InGame;
using Module.StateMachine;
using R3;
using Structure.InGame.Player;

namespace Controller.InGame.Player;

/// <summary>
/// ステートフルなロジックへの型エイリアス
/// </summary>
public class PlayerStateMachine : AbstractAsyncStateMachine<PlayerStateType>
{
    public PlayerStateMachine
    (
        IState<PlayerStateType> state,
        IReadOnlyList<IStateBehaviour<PlayerStateType>> behaviourEntities,
        CompositeDisposable compositeDisposable
    ) : base(state, behaviourEntities, compositeDisposable)
    {
    }
}

public abstract class PlayerStateBehaviourBase : StateBehaviour<PlayerStateType>
{
    protected PlayerStateBehaviourBase
    (
        PlayerStateType playerStateType,
        IMutStateEntity<PlayerStateType> stateEntity
    ) : base(playerStateType, stateEntity)
    {
    }
}

public class PlayerState : AbstractStateType<PlayerStateType>, IResetable
{
    public PlayerState() : base(PlayerStateType.Idle)
    {
    }


    public void Reset()
    {
        ChangeState(EntryState).Forget();
    }
}