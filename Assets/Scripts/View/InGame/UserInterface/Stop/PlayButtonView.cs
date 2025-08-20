using Interface.InGame.UserInterface;
using R3;
using Structure.Utility.Abstraction;

namespace View.InGame.UserInterface.Stop
{
    public class PlayButtonUiView : AbstractButtonView<Unit>, IPlayButtonView
    {
        protected override Unit EventValue => Unit.Default;
        public Observable<Unit> Performed => ButtonSubject;
    }
}