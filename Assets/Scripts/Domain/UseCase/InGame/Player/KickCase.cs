using Domain.IPresenter.InGame.Player;

namespace Domain.UseCase.InGame.Player
{
    public class KickCase
    {
        public KickCase
        (
            IKickPresenter kickPresenter
        )
        {
            KickPresenter = kickPresenter;

            kickPresenter.ClickEvent += type =>
            {

            };
        }
        
        private IKickPresenter KickPresenter { get; }
    }
}