
using Controller.Global.UserInterface;
using Cysharp.Threading.Tasks;
using Interface.Global.Scene;
using Interface.Global.TimeScale;
using Interface.Global.UserInterface;
using Interface.Global.Utility;
using NUnit.Framework;
using R3;
using Structure.Global.TimeScale;

namespace Tests.EditMode.Controller.Global.UserInterface
{
    public class LoadingPanelControllerTest
    {
        // Mocks
        private class MockLoadingPanelView : ILoadingPanelView
        {
            public bool IsShowPanelCalled { get; private set; }
            public bool IsHidePanelCalled { get; private set; }
            public UniTask ShowPanel() { IsShowPanelCalled = true; return UniTask.CompletedTask; }
            public UniTask HidePanel() { IsHidePanelCalled = true; return UniTask.CompletedTask; }
        }

        private class MockSceneLoadEventModel : ISceneLoadEventModel, ISceneLoadSubjectModel
        {
            private readonly Subject<Unit> _startLoadScene = new();
            private readonly Subject<Unit> _endLoadScene = new();
            public Observable<Unit> StartLoadScene => _startLoadScene;
            public Observable<Unit> EndLoadScene => _endLoadScene;
            public Observable<Unit> BeforeSceneLoad => Observable.Empty<Unit>();
            public Observable<Unit> AfterSceneLoad => Observable.Empty<Unit>();
            public Observable<Unit> BeforeNextSceneActivate => Observable.Empty<Unit>();
            public Observable<Unit> AfterNextSceneActivate => Observable.Empty<Unit>();
            public Observable<Unit> BeforeSceneUnLoad => Observable.Empty<Unit>();
            public Observable<Unit> AfterSceneUnLoad => Observable.Empty<Unit>();

            public void SimulateStartLoadScene() => _startLoadScene.OnNext(Unit.Default);
            public void SimulateEndLoadScene() => _endLoadScene.OnNext(Unit.Default);
            public void InvokeStartLoadScene() => _startLoadScene.OnNext(Unit.Default);
            public void InvokeEndLoadScene() => _endLoadScene.OnNext(Unit.Default);
            public void InvokeBeforeSceneLoad() { }
            public void InvokeAfterSceneLoad() { }
            public void InvokeBeforeNextSceneActivate() { }
            public void InvokeAfterNextSceneActivate() { }
            public void InvokeBeforeSceneUnLoad() { }
            public void InvokeAfterSceneUnLoad() { }
        }

        private class MockBlockingOperationModel : IBlockingOperationModel
        {
            public int SpawnCount { get; private set; }
            public OperationHandle SpawnOperation(string context) { SpawnCount++; return new OperationHandle(); }
            public bool IsAnyBlocked() => false;
            public System.Collections.Generic.IReadOnlyList<OperationHandle> GetOperationHandles => null;
        }

        private class MockTimeScaleModel : ITimeScaleModel
        {
            public bool IsResetCalled { get; private set; }
            public void Execute(TimeCommandType timeCommand) { }
            public void Undo() { }
            public void Reset() => IsResetCalled = true;
        }

        private LoadingPanelController _controller;
        private MockLoadingPanelView _loadingPanelView;
        private MockSceneLoadEventModel _sceneLoadEventModel;
        private MockBlockingOperationModel _blockingOperationModel;
        private MockTimeScaleModel _timeScaleModel;
        private CompositeDisposable _compositeDisposable;

        [SetUp]
        public void SetUp()
        {
            _loadingPanelView = new MockLoadingPanelView();
            _sceneLoadEventModel = new MockSceneLoadEventModel();
            _blockingOperationModel = new MockBlockingOperationModel();
            _timeScaleModel = new MockTimeScaleModel();
            _compositeDisposable = new CompositeDisposable();

            _controller = new LoadingPanelController(
                _loadingPanelView, _sceneLoadEventModel, _blockingOperationModel,
                _timeScaleModel, _compositeDisposable
            );
            _controller.Start();
        }

        [TearDown] public void TearDown() => _compositeDisposable.Dispose();

        [Test]
        public void OnStartLoadScene_ResetsTimeScale_ShowsPanel_SpawnsOperation()
        {
            _sceneLoadEventModel.SimulateStartLoadScene();

            Assert.IsTrue(_timeScaleModel.IsResetCalled);
            Assert.IsTrue(_loadingPanelView.IsShowPanelCalled);
            Assert.AreEqual(1, _blockingOperationModel.SpawnCount);
        }

        [Test]
        public void OnEndLoadScene_HidesPanel_SpawnsOperation()
        {
            _sceneLoadEventModel.SimulateEndLoadScene();

            Assert.IsTrue(_loadingPanelView.IsHidePanelCalled);
            Assert.AreEqual(1, _blockingOperationModel.SpawnCount);
        }
    }
}
