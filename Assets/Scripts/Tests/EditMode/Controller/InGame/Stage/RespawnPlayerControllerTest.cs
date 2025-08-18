
using Controller.InGame.Stage;
using Interface.InGame.Player;
using Interface.InGame.Primary;
using Interface.InGame.Stage;
using Module.Option.Runtime;
using NUnit.Framework;
using R3;
using UnityEngine;

namespace Tests.EditMode.Controller.InGame.Stage
{
    public class RespawnPlayerControllerTest
    {
        // Mocks
        private class MockPlayerView : IPlayerView
        {
            public Transform ModelTransform { get; } = new GameObject().transform;
            public Vector2 LinearVelocity => Vector2.zero; public float AngularVelocity => 0f;
            public Observable<Collision2D> CollisionEnterEvent => Observable.Empty<Collision2D>();
            public Vector2? ResetPositionValue { get; private set; }
            public void Activation(bool isActive) { }
            public void ResetPosition(Vector2 position) { ResetPositionValue = position; }
        }

        private class MockLazyPlayerView : ILazyPlayerView { public OnceCell<IPlayerView> PlayerView { get; } = new(); }
        private class MockSpawnPositionView : ISpawnPositionView { public Transform StartPosition { get; } = new GameObject().transform; }
        private class MockFallLineModel : IFallLineModel { public float FallLine { get; set; } = -10f; }

        private RespawnPlayerController _controller;
        private MockLazyPlayerView _lazyPlayerView;
        private MockSpawnPositionView _spawnPositionView;
        private MockFallLineModel _fallLineModel;
        private MockPlayerView _playerView;

        [SetUp]
        public void SetUp()
        {
            _lazyPlayerView = new MockLazyPlayerView();
            _spawnPositionView = new MockSpawnPositionView();
            _fallLineModel = new MockFallLineModel();
            _playerView = new MockPlayerView();

            _controller = new RespawnPlayerController(_lazyPlayerView, _spawnPositionView, _fallLineModel);
        }

        [Test]
        public void Tick_PlayerAboveFallLine_DoesNotRespawn()
        {
            // Arrange
            _lazyPlayerView.PlayerView.Init(_playerView);
            _playerView.ModelTransform.position = new Vector3(0, 0, 0);
            _fallLineModel.FallLine = -10f;

            // Act
            _controller.Tick();

            // Assert
            Assert.IsFalse(_playerView.ResetPositionValue.HasValue);
        }

        [Test]
        public void Tick_PlayerBelowFallLine_RespawnsPlayer()
        {
            // Arrange
            _lazyPlayerView.PlayerView.Init(_playerView);
            _playerView.ModelTransform.position = new Vector3(0, -20, 0);
            _fallLineModel.FallLine = -10f;
            _spawnPositionView.StartPosition.position = new Vector3(5, 5, 5);

            // Act
            _controller.Tick();

            // Assert
            Assert.IsTrue(_playerView.ResetPositionValue.HasValue);
            Assert.AreEqual((Vector2)_spawnPositionView.StartPosition.position, _playerView.ResetPositionValue.Value);
        }

        [Test]
        public void Tick_PlayerViewNotInitialized_DoesNotThrow()
        {
            // Act & Assert
            Assert.DoesNotThrow(() => _controller.Tick());
        }
    }
}
