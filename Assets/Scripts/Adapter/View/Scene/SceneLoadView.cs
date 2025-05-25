using Adapter.IView.Scene;
using Structure.Scene;
using UnityEngine.SceneManagement;

namespace Adapter.View.Scene
{
    public class SceneLoadView: ISceneLoadView
    {
        public void Load(SceneType sceneType)
        {
            SceneManager.LoadScene(sceneType.ToSceneName());
        }
    }
}