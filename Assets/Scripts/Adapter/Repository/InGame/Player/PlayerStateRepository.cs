using Module.StateMachine;
using Structure.InGame.Player;

namespace Adapter.Repository.InGame.Player
{
    public class PlayerStateRepository : AbstractStateType<PlayerStateType>
    {
        public PlayerStateRepository() : base(PlayerStateType.Idle)
        {
        }
    }
}