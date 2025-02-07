using DataUtil.InGame.Player;
using Module.StateMachine;

namespace Domain.Entity.InGame.Player.State
{
    public class PlayerStateMachine: StateMachine<PlayerStateType>
    {
        public PlayerStateMachine(PlayerStateType owner) : base(owner)
        {
        }
    }
}