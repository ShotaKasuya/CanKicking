using Interface.InGame.UserInterface;
using R3;

namespace View.InGame.UserInterface.Stop
{
    public class PlayButtonView : AbstractButtonView<Unit>, IPlayButtonView
    {
        protected override Unit EventValue => Unit.Default;
        public Observable<Unit> Performed => ButtonSubject;
    }
}