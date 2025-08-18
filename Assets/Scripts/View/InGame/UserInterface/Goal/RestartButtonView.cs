using Interface.InGame.UserInterface;
using R3;
using Structure.Utility.Abstraction;
using UnityEngine.SceneManagement;
using View.InGame.UserInterface.Stop;

namespace View.InGame.UserInterface.Goal
{
    public class RestartButtonView : AbstractButtonView<string>, IGoal_RestartButtonView
    {
        protected override string EventValue => SceneManager.GetActiveScene().path;
        public Observable<string> Performed => ButtonSubject;
    }
}