using System;
using Cysharp.Threading.Tasks;
using Interface.Global.Scene;
using Interface.OutGame.Title;
using R3;
using VContainer.Unity;

namespace Controller.OutGame
{
    /// <summary>
    /// タイトル画面のコントローラー
    /// </summary>
    public class TitleController: IStartable, IDisposable
    {
        public TitleController
        (
            IStartGameView startGameView,
            ISceneLoaderView sceneLoadView,
            IGameStartSceneModel gameStartSceneModel
        )
        {
            StartGameView = startGameView;
            SceneLoadView = sceneLoadView;
            GameStartSceneModel = gameStartSceneModel;
            
            Disposable = new CompositeDisposable();
        }
        
        public void Start()
        {
            StartGameView.StartEvent
                .Subscribe(this, (_, controller) => controller.OnStart())
                .AddTo(Disposable);
        }
        
        private void OnStart()
        {
            return;
            var scene = GameStartSceneModel.GetStartScene();
            SceneLoadView.LoadScene(scene).Forget();
        }
        
        private CompositeDisposable Disposable { get; }
        private IStartGameView StartGameView { get; }
        private ISceneLoaderView SceneLoadView { get; }
        private IGameStartSceneModel GameStartSceneModel { get; }
        
        public void Dispose()
        {
            Disposable.Dispose();
        }
    }
}