using Interface.InGame.UserInterface;
using Module.SceneReference;
using R3;
using UnityEngine.SceneManagement;
using View.InGame.UserInterface.Stop;

namespace View.InGame.UserInterface.Goal
{
    public class RestartButtonView : AbstractButtonView<SceneReference>, IGoal_RestartButtonView
    {
        protected override SceneReference EventValue => _currentScene;
        public Observable<SceneReference> Performed => ButtonSubject;

        private SceneReference _currentScene;

        private void Start()
        {
            _currentScene = new SceneReference(SceneManager.GetActiveScene().name);
        }
    }
}