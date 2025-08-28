using System.Text;
using System.Threading;
using Cysharp.Threading.Tasks;
using Interface.Logic.Global;
using Interface.Model.Global;
using Module.SceneReference.Runtime;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer;

namespace Installer.Global
{
    public partial class GlobalLocator
    {
        [SerializeField] private bool observeBlockingOperation;

        private void DebugStarter()
        {
#if UNITY_EDITOR
            CheckEntryPoint().Forget();
            if (observeBlockingOperation)
            {
                ObserveBlockingOperation(destroyCancellationToken).Forget();
            }
#endif
        }

        private const string EntryPointScene = "_init";
        private const string EnvironmentSceneHead = "Env_";
        private const string UiSceneHead = "UI_";
        private const string Primary = "Primary";
        private const string Environment = "Environment";
        private const string UserInterface = "UI";

        private async UniTask CheckEntryPoint()
        {
            var primarySceneModel = Container.Resolve<IPrimarySceneModel>();
            var loadPrimarySceneLogic = Container.Resolve<ILoadPrimarySceneLogic>();
            var currentScene = SceneManager.GetActiveScene().name;
            var currentScenePath = SceneManager.GetActiveScene().path;

            Debug.Log($"current: {currentScene}, entry point: {EntryPointScene}");
            if (EntryPointScene == currentScene)
            {
                return;
            }

            primarySceneModel.ToggleCurrentScene(SceneContext.SceneManagerContext(
                null, currentScenePath
            ));

            if (currentScene.StartsWith(EnvironmentSceneHead))
            {
                var nextScenePath = currentScenePath.Replace(Environment, Primary);
                nextScenePath = nextScenePath.Replace(EnvironmentSceneHead, string.Empty);
                await loadPrimarySceneLogic.ChangeScene(nextScenePath);
                return;
            }

            if (currentScene.StartsWith(UiSceneHead))
            {
                var nextScenePath = currentScenePath.Replace(UserInterface, Primary);
                nextScenePath = nextScenePath.Replace(UiSceneHead, string.Empty);
                await loadPrimarySceneLogic.ChangeScene(nextScenePath);
            }
        }

        private async UniTask ObserveBlockingOperation(CancellationToken cancellationToken)
        {
            var model = Container.Resolve<IBlockingOperationModel>();

            while (true)
            {
                var operations = model.GetOperationHandles;
                var logger = new StringBuilder();

                for (int i = 0; i < operations.Count; i++)
                {
                    var handle = operations[i];
                    logger.AppendLine(handle.ToString());
                }

                Debug.Log(logger.ToString());
                await UniTask.WaitForSeconds(1f, cancellationToken: cancellationToken);
            }
        }
    }
}