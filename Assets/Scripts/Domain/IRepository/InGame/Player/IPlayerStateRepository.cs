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

    public interface IRotationStateRepository
    {
        public float RotationAngle { get; }
        public RotationStateType Read();
        public void Toggle();
        public void Set(RotationStateType type);
    }
}