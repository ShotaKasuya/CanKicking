using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Controller.InGame;
using NUnit.Framework;
using R3;
using UnityEngine;
using Module.Option.Runtime;
using Module.SceneReference.Runtime;
using Interface.Model.Global;
using Interface.Model.InGame;
using Interface.View.InGame;
using Tests.Mock.InGame.Primary;

namespace Tests.EditMode.Controller.InGame
{
    public class GameStartControllerTest
    {
        // Mocks
        private class MockSpawnPositionView : ISpawnPositionView
        {
            public Transform StartPosition { get; set; }
            // The controller's WaitUntil predicate checks this property.
            public bool IsInitialized { get; set; }

            public MockSpawnPositionView()
            {
                StartPosition = new GameObject().transform;
            }
        }

        private class MockPlayerView : IPlayerView
        {
            public Transform ModelTransform { get; set; }
            public Vector2 LinearVelocity => Vector2.zero;
            public float AngularVelocity => 0f;
            public Observable<Collision2D> CollisionEnterEvent => Observable.Empty<Collision2D>();
            public bool IsActive { get; private set; }
            // The controller's WaitUntil predicate checks this property.
            public bool IsInitialized { get; set; }

            public MockPlayerView()
            {
                ModelTransform = new GameObject().transform;
            }

            public void Activation(bool isActive)
            {
                IsActive = isActive;
            }

            public void ResetPosition(Pose pose)
            {
                ModelTransform.position = pose.position;
                ModelTransform.rotation = pose.rotation;
            }
        }

        private class MockLazyStartPositionView : ILazyStartPositionView
        {
            public OnceCell<ISpawnPositionView> StartPosition { get; } = new();
        }

        private class MockLazyPlayerView : ILazyPlayerView
        {
            public OnceCell<IPlayerView> PlayerView { get; } = new();
        }

        private class MockClearRecordModel : IClearRecordModel
        {
            public readonly Dictionary<string, int> SavedData = new();

            public void Save(string key, int jumpCount)
            {
                SavedData[key] = jumpCount;
            }

            public Option<int> Load(string key) => SavedData.TryGetValue(key, out var value)
                ? Option<int>.Some(value)
                : Option<int>.None();
        }

        private class MockGoalEventModel : IGoalEventModel
        {
            private readonly Subject<Unit> _goalEventSubject = new();
            public Observable<Unit> GoalEvent => _goalEventSubject;
            public void SimulateGoal() => _goalEventSubject.OnNext(Unit.Default);
        }

        private class MockPrimarySceneModel : IPrimarySceneModel
        {
            private SceneContext _currentSceneContext;

            public SceneContext ToggleCurrentScene(SceneContext sceneInstance)
            {
                _currentSceneContext = sceneInstance;
                return sceneInstance;
            }

            public SceneContext GetCurrentSceneContext => _currentSceneContext;

            public void SetCurrentScene(string path)
            {
                _currentSceneContext = SceneContext.SceneManagerContext(null, path);
            }
        }

        private GameStartController _controller;
        private MockLazyStartPositionView _lazyStartPositionView;
        private MockLazyPlayerView _lazyPlayerView;
        private MockKickCountModel _kickCountModel;
        private MockClearRecordModel _clearRecordModel;
        private MockGoalEventModel _goalEventModel;
        private MockPrimarySceneModel _primarySceneModel;
        private CompositeDisposable _compositeDisposable;

        [SetUp]
        public void SetUp()
        {
            _lazyStartPositionView = new MockLazyStartPositionView();
            _lazyPlayerView = new MockLazyPlayerView();
            _kickCountModel = new MockKickCountModel();
            _clearRecordModel = new MockClearRecordModel();
            _goalEventModel = new MockGoalEventModel();
            _primarySceneModel = new MockPrimarySceneModel();
            _compositeDisposable = new CompositeDisposable();

            _controller = new GameStartController(
                _lazyStartPositionView,
                _lazyPlayerView,
                _kickCountModel,
                _clearRecordModel,
                _goalEventModel,
                _primarySceneModel,
                _compositeDisposable
            );
        }

        [TearDown]
        public void TearDown()
        {
            _compositeDisposable.Dispose();
        }

        [Test]
        public async Task Initialize_WhenViewsAreReady_SetsPlayerPosition()
        {
            // Arrange
            var playerView = new MockPlayerView { IsInitialized = true };
            var startPositionView = new MockSpawnPositionView { IsInitialized = true };
            startPositionView.StartPosition.position = new Vector3(10, 20, 0);

            _lazyPlayerView.PlayerView.Init(playerView);
            _lazyStartPositionView.StartPosition.Init(startPositionView);

            // Act
            _controller.Initialize();
            // Initializeはasync voidメソッドを開始するため、
            // テスト内で処理が進むように1フレーム待機します。
            await Task.Delay(TimeSpan.FromSeconds(0.25));

            // Assert
            Assert.AreEqual(startPositionView.StartPosition.position, playerView.ModelTransform.position);
        }

        [Test]
        public void OnGoalEvent_SavesRecord()
        {
            // Arrange
            const string sceneKey = "TestScenePath";
            const int jumps = 5;
            _primarySceneModel.SetCurrentScene(sceneKey);
            for (int i = 0; i < jumps; i++) _kickCountModel.Inc();

            _controller.Initialize();

            // Act
            _goalEventModel.SimulateGoal();

            // Assert
            Assert.IsTrue(_clearRecordModel.SavedData.ContainsKey(sceneKey));
            Assert.AreEqual(jumps, _clearRecordModel.SavedData[sceneKey]);
        }
    }
}