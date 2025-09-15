using System;
using System.Threading;
using System.Threading.Tasks;
using Controller.InGame.UserInterface;
using NUnit.Framework;
using R3;
using Structure.InGame.UserInterface;
using Tests.Mock.InGame;
using Tests.Mock.InGame.Primary;
using UnityEngine;

namespace Tests.EditMode.Controller.InGame.UserInterface
{
    public class NormalStateControllerTest
    {
        private NormalStateController _controller;
        private MockNormalUiView _normalUiView;
        private MockLazyPlayerView _lazyPlayerView;
        private MockLazyBaseHeightView _lazyBaseHeightView;
        private MockLazyGoalHeightView _lazyGoalHeightView;
        private MockStopButtonView _stopButtonView;
        private MockProgressUiView _progressUiView;
        private MockKickCountUiView _kickCountUiView;
        private MockGoalEventModel _goalEventModel;
        private MockKickCountModel _kickCountModel;
        private MockUiStateEntity _stateEntity;
        private CompositeDisposable _compositeDisposable;

        [SetUp]
        public void SetUp()
        {
            _normalUiView = new MockNormalUiView();
            _lazyPlayerView = new MockLazyPlayerView();
            _lazyBaseHeightView = new MockLazyBaseHeightView();
            _lazyGoalHeightView = new MockLazyGoalHeightView();
            _stopButtonView = new MockStopButtonView();
            _progressUiView = new MockProgressUiView();
            _kickCountUiView = new MockKickCountUiView();
            _goalEventModel = new MockGoalEventModel();
            _kickCountModel = new MockKickCountModel();
            _stateEntity = new MockUiStateEntity(UserInterfaceStateType.Normal);
            _compositeDisposable = new CompositeDisposable();

            _controller = new NormalStateController(
                _normalUiView, _lazyPlayerView, _lazyBaseHeightView, _lazyGoalHeightView,
                _stopButtonView, _progressUiView, _kickCountUiView, _goalEventModel,
                _kickCountModel, _compositeDisposable, _stateEntity
            );
            _controller.Start();
        }

        [TearDown]
        public void TearDown() => _compositeDisposable.Dispose();

        [Test]
        public async Task OnEnter_ShowsNormalUi()
        {
            await _controller.OnEnter(CancellationToken.None);
            Assert.IsTrue(_normalUiView.IsShown);
        }

        [Test]
        public async Task OnExit_HidesNormalUi()
        {
            await _controller.OnExit(CancellationToken.None);
            Assert.IsFalse(_normalUiView.IsShown);
        }

        [Test]
        public async Task OnStopButtonClick_ChangesStateToStop()
        {
            _stopButtonView.SimulateClick(Unit.Default);
            await Task.Delay(TimeSpan.FromSeconds(0.25));
            Assert.AreEqual(UserInterfaceStateType.Stop, _stateEntity.CurrentState);
        }

        [Test]
        public async Task OnGoalEvent_ChangesStateToGoal()
        {
            _goalEventModel.SimulateGoal();
            await Task.Delay(TimeSpan.FromSeconds(0.25));
            Assert.AreEqual(UserInterfaceStateType.Goal, _stateEntity.CurrentState);
        }

        [Test]
        public void OnJumpCountChanged_UpdatesUi()
        {
            _kickCountModel.Inc();
            Assert.AreEqual(1, _kickCountUiView.Count);
        }

        [Test]
        public void StateUpdate_SetsCorrectProgress()
        {
            var playerView = new MockPlayerView();
            playerView.ModelTransform.position = new Vector3(0, 50, 0);
            _lazyPlayerView.PlayerView.Init(playerView);
            _lazyBaseHeightView.BaseHeight.Init(0f);
            _lazyGoalHeightView.GoalHeight.Init(100f);

            _controller.StateUpdate(0.1f);

            Assert.AreEqual(0.5f, _progressUiView.Progress);
        }
    }
}