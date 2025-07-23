using System.Text;
using Cysharp.Threading.Tasks;
using Interface.Global.Scene;
using Interface.Global.Utility;
using Module.SceneReference;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer;

namespace Installer.Global
{
    public partial class GlobalLocator
    {
        [SerializeField] private bool observeBlockingOperation;

        private const string EntryPointScene = "_init";

        private void DebugStarter()
        {
#if UNITY_EDITOR
            CheckEntryPoint().Forget();
            if (observeBlockingOperation)
            {
                ObserveBlockingOperation().Forget();
            }
#endif
        }

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

            primarySceneModel.ToggleCurrentScene(new SceneContext(
                SceneType.SceneManager, default, null,
                currentScenePath));

            await loadPrimarySceneLogic.ChangeScene(currentScenePath);
        }

        private async UniTask ObserveBlockingOperation()
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
                await UniTask.WaitForSeconds(1f);
            }
        }
    }
}