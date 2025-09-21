using Interface.View.InGame.UserInterface;
using R3;
using View.Utility;

namespace View.InGame.UserInterface.Stop
{
    public class PlayButtonUiView : AbstractButtonView<Unit>, IPlayButtonView
    {
        protected override Unit EventValue => Unit.Default;
        public Observable<Unit> Performed => ButtonSubject;
    }
}