using Controller.Global.UserInterface;
using NUnit.Framework;
using R3;
using Tests.Mock.Controller.Global.UserInterface;

namespace Tests.EditMode.Controller.Global.UserInterface
{
    public class LoadingPanelControllerTest
    {
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

        [TearDown]
        public void TearDown() => _compositeDisposable.Dispose();

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