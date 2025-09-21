using Interface.View.InGame.UserInterface;
using R3;
using UnityEngine.SceneManagement;
using View.Utility;

namespace View.InGame.UserInterface.Goal
{
    public class RestartButtonView : AbstractButtonView<string>, IGoal_RestartButtonView
    {
        protected override string EventValue => SceneManager.GetActiveScene().path;
        public Observable<string> Performed => ButtonSubject;
    }
}