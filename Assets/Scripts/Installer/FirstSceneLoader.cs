using Cysharp.Threading.Tasks;
using Interface.Global.Scene;
using Module.SceneReference;
using Module.SceneReference.AeLa.Utilities;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer;
using VContainer.Unity;

namespace Installer
{
    public class FirstSceneLoader : LifetimeScope
    {
        [SerializeField] private SceneField sceneField;

        private void Start()
        {
            Load().Forget();
        }

        private async UniTask Load()
        {
            var loadLogic = Container.Resolve<ILoadPrimarySceneLogic>();
            var primarySceneModel = Container.Resolve<IPrimarySceneModel>();

            primarySceneModel.ToggleCurrentScene(new SceneReleaseContext(
                SceneType.SceneManager, default, null,
                SceneManager.GetActiveScene().name));

            await loadLogic.ChangeScene(sceneField);
        }
    }
}