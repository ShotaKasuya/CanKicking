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
            ISceneLoadEventModel sceneLoadEventModel
        )
        {
            PrimarySceneModel = primarySceneModel;
            BlockingOperationModel = blockingOperationModel;
            SceneLoaderView = sceneLoaderView;
            SceneLoadEventModel = sceneLoadEventModel;
        }

        public async UniTask ChangeScene(string scenePath)
        {
            await UniTask.WaitUntil(this, logic => logic.BlockingOperationModel.IsAnyBlocked());

            SceneLoadEventModel.InvokeBeforeSceneLoad();

            var sceneInstance = await SceneLoaderView.LoadScene(scenePath);
            SceneLoadEventModel.InvokeBeforeNextSceneActivate();
            await SceneLoaderView.ActivateAsync(sceneInstance);

            SceneLoadEventModel.InvokeBeforeSceneUnLoad();
            var prevSceneInstance = PrimarySceneModel.ToggleCurrentScene(sceneInstance);
            await SceneLoaderView.UnLoadScene(prevSceneInstance);
            SceneLoadEventModel.InvokeAfterSceneUnLoad();
        }

        private IPrimarySceneModel PrimarySceneModel { get; }
        private IBlockingOperationModel BlockingOperationModel { get; }
        private INewSceneLoaderView SceneLoaderView { get; }
        private ISceneLoadEventModel SceneLoadEventModel { get; }
    }
}