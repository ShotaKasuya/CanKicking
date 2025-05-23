using Module.StateMachine;
using Structure.InGame.Player;

namespace Domain.IRepository.InGame.Player
{
    public interface IPlayerStateRepository : IState<PlayerStateType>
    {
    }

    public interface IMutPlayerStateRepository : IMutStateEntity<PlayerStateType>
    {
    }
}