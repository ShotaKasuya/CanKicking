using System;
using System.Threading;
using System.Threading.Tasks;
using Controller.InGame.UserInterface;
using NUnit.Framework;
using R3;
using Structure.Global.TimeScale;
using Structure.InGame.Player;
using Structure.InGame.UserInterface;
using Tests.EditMode.Logic.Global;
using Tests.EditMode.Logic.InGame;
using Tests.Mock;
using Tests.Mock.Global;
using Tests.Mock.InGame;
using MockUiStateEntity = Tests.Mock.MockUiStateEntity;

namespace Tests.EditMode.Controller.InGame.UserInterface
{
    public class StopStateControllerTest
    {
        private StopStateController _controller;
        private MockStopUiView _stopUiView;
        private MockPlayButtonView _playButtonView;
        private MockStageSelectButtonView _stageSelectButtonView;
        private MockRestartButtonView _restartButtonView;
        private MockLoadPrimarySceneLogic _loadPrimarySceneLogic;
        private MockTimeScaleModel _timeScaleModel;
        private MockGameRestartLogic _gameRestartLogic;
        private MockPlayerStateEntity _playerStateEntity;
        private MockUiStateEntity _uiStateEntity;
        private CompositeDisposable _compositeDisposable;

        [SetUp]
        public void SetUp()
        {
            _stopUiView = new MockStopUiView();
            _playButtonView = new MockPlayButtonView();
            _stageSelectButtonView = new MockStageSelectButtonView();
            _restartButtonView = new MockRestartButtonView();
            _loadPrimarySceneLogic = new MockLoadPrimarySceneLogic();
            _timeScaleModel = new MockTimeScaleModel();
            _gameRestartLogic = new MockGameRestartLogic();
            _playerStateEntity = new MockPlayerStateEntity(default);
            _uiStateEntity = new MockUiStateEntity(UserInterfaceStateType.Stop);
            _compositeDisposable = new CompositeDisposable();

            _controller = new StopStateController(
                _stopUiView, _playButtonView, _stageSelectButtonView, _restartButtonView,
                _loadPrimarySceneLogic, _timeScaleModel, _gameRestartLogic, _compositeDisposable,
                _playerStateEntity, _uiStateEntity
            );
            _controller.Start();
        }

        [TearDown]
        public void TearDown() => _compositeDisposable.Dispose();

        [Test]
        public async Task OnEnter_SetsPlayerState_SetsTimeScale_ShowsUi()
        {
            await _controller.OnEnter(CancellationToken.None);
            Assert.AreEqual(PlayerStateType.Stopping, _playerStateEntity.CurrentState);
            Assert.AreEqual(TimeCommandType.Stop, _timeScaleModel.ExecutedCommand);
            Assert.IsTrue(_stopUiView.IsShown);
        }

        [Test]
        public async Task OnExit_ResetsPlayerState_UndoesTimeScale_HidesUi()
        {
            await _controller.OnExit(CancellationToken.None);
            Assert.AreEqual(PlayerStateType.Idle, _playerStateEntity.CurrentState);
            Assert.IsTrue(_timeScaleModel.IsUndoCalled);
            Assert.IsFalse(_stopUiView.IsShown);
        }

        [Test]
        public async Task OnPlayButtonClick_ChangesStateToNormal()
        {
            _playButtonView.SimulateClick(Unit.Default);
            await Task.Delay(TimeSpan.FromSeconds(0.25));
            Assert.AreEqual(UserInterfaceStateType.Normal, _uiStateEntity.CurrentState);
        }

        [Test]
        public void OnRestartButtonClick_RestartsGame()
        {
            _restartButtonView.SimulateClick("any");
            Assert.IsTrue(_gameRestartLogic.IsRestarted);
        }

        [Test]
        public void OnStageSelectButtonClick_ChangesScene()
        {
            var scene = "NewScene";
            _stageSelectButtonView.SimulateClick(scene);
            Assert.AreEqual(scene, _loadPrimarySceneLogic.CalledScenePath);
        }
    }
}