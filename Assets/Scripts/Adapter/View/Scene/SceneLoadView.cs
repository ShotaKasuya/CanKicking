using Adapter.IView.Scene;
using UnityEngine.SceneManagement;

namespace Detail.View.Scene
{
    public class SceneLoadView: ISceneLoadView
    {
        public void Load(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}