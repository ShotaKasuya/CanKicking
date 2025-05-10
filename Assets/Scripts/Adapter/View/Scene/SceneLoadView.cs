using Adapter.IView.Scene;
using UnityEngine.SceneManagement;

namespace Adapter.View.Scene
{
    public class SceneLoadView: ISceneLoadView
    {
        public void Load(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}