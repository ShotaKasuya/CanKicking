using Domain.IRepository.InGame.Player;
using Module.StateMachine;
using Structure.InGame.Player;

namespace Adapter.Repository.InGame.Player
{
    public class PlayerStateRepository : AbstractStateType<PlayerStateType>, IMutPlayerStateRepository, IPlayerStateRepository
    {
        public PlayerStateRepository() : base(PlayerStateType.Idle)
        {
        }
    }
}