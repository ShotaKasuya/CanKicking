using System;
using Controller.InGame.UserInterface;
using NUnit.Framework;
using R3;
using Structure.InGame.UserInterface;
using System.Threading.Tasks;

namespace Tests.EditMode.Controller.InGame.UserInterface
{
    public class UserInterfaceStateTest
    {
        private UserInterfaceState _state;

        [SetUp]
        public void SetUp()
        {
            _state = new UserInterfaceState();
        }

        // テストケース1: 初期状態は 'Normal' であること
        [Test]
        public void InitialState_IsNormal()
        {
            Assert.AreEqual(UserInterfaceStateType.Normal, _state.CurrentState);
        }

        // テストケース2: ChangeStateで現在の状態が更新されること
        [Test]
        public async Task ChangeState_UpdatesCurrentState()
        {
            // Arrange
            await _state.ChangeState(UserInterfaceStateType.Goal);

            // Assert
            Assert.AreEqual(UserInterfaceStateType.Goal, _state.CurrentState);
        }

        // テストケース3: Resetで状態が 'Normal' に戻ること
        [Test]
        public async Task Reset_ChangesStateToNormal()
        {
            // Arrange
            await _state.ChangeState(UserInterfaceStateType.Stop);

            // Act
            _state.Reset();
            await Task.Delay(TimeSpan.FromSeconds(0.5));

            // Assert
            Assert.AreEqual(_state.EntryState, _state.CurrentState);
        }

        // テストケース4: 状態の変更が購読者に通知されること
        [Test]
        public async Task CurrentState_OnValueChanged_NotifiesSubscriber()
        {
            // Arrange
            var receivedState = UserInterfaceStateType.Normal;
            var disposable = _state.StateEnterObservable
                .Subscribe(type => receivedState = type);

            // Act
            await _state.ChangeState(UserInterfaceStateType.Goal);

            // Assert
            Assert.AreEqual(UserInterfaceStateType.Goal, receivedState);
            disposable.Dispose();
        }
    }
}