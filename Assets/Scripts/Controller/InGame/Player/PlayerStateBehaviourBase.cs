using System.Collections.Generic;
using Interface.Logic.InGame;
using Module.StateMachine;
using R3;
using Structure.InGame.Player;

namespace Controller.InGame.Player;

/// <summary>
/// ステートフルなロジックへの型エイリアス
/// </summary>
public class PlayerStateMachine : AbstractStateMachine<PlayerStateType>
{
    public PlayerStateMachine
    (
        IStateType<PlayerStateType> stateType,
        IReadOnlyList<IStateBehaviour<PlayerStateType>> behaviours,
        CompositeDisposable compositeDisposable
    ) : base(stateType, behaviours, compositeDisposable)
    {
    }
}

public abstract class PlayerStateBehaviourBase : AbstractStateBehaviour<PlayerStateType>
{
    protected PlayerStateBehaviourBase
    (
        PlayerStateType playerStateType,
        IMutStateType<PlayerStateType> innerState
    ) : base(playerStateType, innerState)
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
        ChangeState(EntryState);
    }
}