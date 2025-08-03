using System.Collections.Generic;
using Interface.InGame.Primary;
using Module.StateMachine;
using ModuleExtension.StateMachine;
using Structure.InGame.Player;

namespace Controller.InGame.Player;

/// <summary>
/// ステートフルなロジックへの型エイリアス
/// </summary>
public class PlayerStateMachine : StateMachineBase<PlayerStateType>
{
    public PlayerStateMachine
    (
        IState<PlayerStateType> state,
        IReadOnlyList<IStateBehaviour<PlayerStateType>> behaviourEntities
    ) : base(state, behaviourEntities)
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

public class PlayerState : AbstractStateType<PlayerStateType>,  IResetable
{
    public PlayerState() : base(EntryState)
    {
    }

    private const PlayerStateType EntryState = PlayerStateType.Idle;
    public void Reset()
    {
        ChangeState(EntryState);
    }
}