using DataUtil.InGame.Player;
using Domain.IPresenter.Util;
using Domain.IUseCase.InGame;
using Module.StateMachine;

namespace Domain.UseCase.InGame.Player
{
    public class PlayerIdleCase : PlayerStateBehaviourBase
    {
        public PlayerIdleCase
        (
            ITouchEndPresenter touchEndPresenter,
            PlayerStateType playerStateType,
            IMutStateEntity<PlayerStateType> stateEntity
        ) : base(playerStateType, stateEntity)
        {
            TouchEndPresenter = touchEndPresenter;
        }

        public override void StateUpdate(float deltaTime)
        {
            if (!TouchEndPresenter.Pool().TryGetValue(out var touchEnd)) return;
            
            
        }
        
        private ITouchEndPresenter TouchEndPresenter { get; }
    }
}