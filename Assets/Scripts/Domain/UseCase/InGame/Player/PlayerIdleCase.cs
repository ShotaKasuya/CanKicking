using Domain.IPresenter.Util;
using Domain.IUseCase.InGame;
using Module.StateMachine;
using Structure.InGame.Player;

namespace Domain.UseCase.InGame.Player
{
    public class PlayerIdleCase : PlayerStateBehaviourBase
    {
        public PlayerIdleCase
        (
            ITouchPresenter touchPresenter,
            IMutStateEntity<PlayerStateType> stateEntity
        ) : base(PlayerStateType.Idle, stateEntity)
        {
            TouchPresenter = touchPresenter;
        }

        public override void OnEnter()
        {
            TouchPresenter.OnTouch += _ => ToCharge();
        }

        public override void OnExit()
        {
            TouchPresenter.OnTouch -= _ => ToCharge();
        }

        private void ToCharge()
        {
            StateEntity.ChangeState(PlayerStateType.Aiming);
        }


        private ITouchPresenter TouchPresenter { get; }
    }
}