using Interface.View.InGame.UserInterface;
using R3;
using Structure.Utility.Abstraction;
using UnityEngine.SceneManagement;

namespace View.InGame.UserInterface.Stop
{
    public class RestartButtonView: AbstractButtonView<string>, IStop_RestartButtonView
    {
        protected override string EventValue => SceneManager.GetActiveScene().path;
        public Observable<string> Performed => ButtonSubject;
    }
}