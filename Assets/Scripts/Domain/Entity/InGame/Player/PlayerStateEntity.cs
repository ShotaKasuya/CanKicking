using DataUtil.InGame.Player;
using Domain.IEntity.InGame.Player;

namespace Domain.Entity.InGame.Player
{
    public class PlayerStateEntity: IPlayerStateEntity
    {
        public PlayerStateType State { get; private set; }

        public bool IsInState(PlayerStateType state)
        {
            return State == state;
        }

        public void ChangeState(PlayerStateType state)
        {
            State = state;
        }
    }
}