using Cysharp.Threading.Tasks;
using Interface.Global.Scene;
using Interface.Global.Utility;

namespace Logic.Global.Scene
{
    public class ChangePrimarySceneLogic : ISceneChangeLogic
    {
        public ChangePrimarySceneLogic
        (
            IPrimarySceneModel primarySceneModel,
            IBlockingOperationModel blockingOperationModel,
            INewSceneLoaderView sceneLoaderView,
            ISceneLoadEventView sceneLoadEventView
        )
        {
            PrimarySceneModel = primarySceneModel;
            BlockingOperationModel = blockingOperationModel;
            SceneLoaderView = sceneLoaderView;
            SceneLoadEventView = sceneLoadEventView;
        }

        public async UniTask ChangeScene(string scenePath)
        {
            await UniTask.WaitUntil(this, logic => logic.BlockingOperationModel.IsAnyBlocked());

            SceneLoadEventView.InvokeBeforeSceneLoad();

            var sceneInstance = await SceneLoaderView.LoadScene(scenePath);
            SceneLoadEventView.InvokeBeforeNextSceneActivate();
            await SceneLoaderView.ActivateAsync(sceneInstance);

            SceneLoadEventView.InvokeBeforeSceneUnLoad();
            var prevSceneInstance = PrimarySceneModel.ToggleCurrentScene(sceneInstance);
            await SceneLoaderView.UnLoadScene(prevSceneInstance);
            SceneLoadEventView.InvokeAfterSceneUnLoad();
        }

        private IPrimarySceneModel PrimarySceneModel { get; }
        private IBlockingOperationModel BlockingOperationModel { get; }
        private INewSceneLoaderView SceneLoaderView { get; }
        private ISceneLoadEventView SceneLoadEventView { get; }
    }
}