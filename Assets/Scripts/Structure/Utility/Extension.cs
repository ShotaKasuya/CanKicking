using Cysharp.Threading.Tasks;
using Module.SceneReference;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;
using VContainer;

namespace Structure.Utility;

public interface IRegisterable
{
    public void Register(IContainerBuilder builder);
}

public static class Extension
{
    public static async UniTask LoadAsync(this SceneReference sceneReference)
    {
        switch (sceneReference.SceneType)
        {
            case SceneType.Local:
                await SceneManager.LoadSceneAsync(sceneReference.SceneName, LoadSceneMode.Single).ToUniTask();
                break;
            case SceneType.Addressables:
                await Addressables.LoadSceneAsync(sceneReference.SceneName).ToUniTask();
                break;
        }
    }
}