// using System;
// using R3;
// using Structure.Scene;
// using VContainer.Unity;
//
// namespace Domain.Controller.OutGame
// {
//     public class TitleController: IStartable, IDisposable
//     {
//         public TitleController
//         (
//             IStartGameView startGameView,
//             ISceneLoadView sceneLoadView
//         )
//         {
//             StartGameView = startGameView;
//             SceneLoadView = sceneLoadView;
//             Disposable = new CompositeDisposable();
//         }
//         
//         public void Start()
//         {
//             StartGameView.StartEvent
//                 .Subscribe(_ => OnStart())
//                 .AddTo(Disposable);
//         }
//         
//         private void OnStart()
//         {
//             SceneLoadView.Load(SceneType.StageSelect);
//         }
//         
//         private CompositeDisposable Disposable { get; }
//         private IStartGameView StartGameView { get; }
//         private ISceneLoadView SceneLoadView { get; }
//         
//         public void Dispose()
//         {
//             Disposable.Dispose();
//         }
//     }
// }