using Interface.View.InGame.UserInterface;
using R3;
using Structure.Utility.Abstraction;

namespace View.InGame.UserInterface.Normal
{
    public class StopButtonView : AbstractButtonView<Unit>, IStopButtonView
    {
        public Observable<Unit> Performed => ButtonSubject;
        protected override Unit EventValue => Unit.Default;
    }
}