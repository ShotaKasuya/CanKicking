using Domain.IEntity.InGame.Player;
using Domain.IPresenter.InGame.Player;
using Module.Installer;

namespace Domain.UseCase.InGame.Player
{
    public class PlayerStateChangeCase: ITickable
    {
        public PlayerStateChangeCase
        (
            IPlayerStateEntity playerStateEntity,
            IPlayerSpeedPresenter playerSpeedPresenter
        )
        {
            StateEntity = playerStateEntity;
            SpeedPresenter = playerSpeedPresenter;
        }
        
        public void Tick(float deltaTime)
        {
        }

        private IPlayerStateEntity StateEntity { get; }
        private IPlayerSpeedPresenter SpeedPresenter { get; }
    }
}