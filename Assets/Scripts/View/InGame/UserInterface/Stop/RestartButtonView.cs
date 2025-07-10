using Interface.InGame.UserInterface;
using Module.SceneReference;
using R3;
using UnityEngine.SceneManagement;

namespace View.InGame.UserInterface.Stop
{
    public class RestartButtonView: AbstractButtonView<SceneReference>, IStop_RestartButtonView
    {
        protected override SceneReference EventValue => _currentScene;
        public Observable<SceneReference> Performed => ButtonSubject;

        private SceneReference _currentScene;

        private void Start()
        {
            _currentScene = new SceneReference(SceneType.Addressable, SceneManager.GetActiveScene().path);
        }
    }
}