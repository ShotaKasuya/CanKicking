using Controller.InGame.UserInterface;
using NUnit.Framework;
using Structure.InGame.UserInterface;

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
            Assert.AreEqual(UserInterfaceStateType.Normal, _state.State);
        }

        // テストケース2: ChangeStateで現在の状態が更新されること
        [Test]
        public void ChangeState_UpdatesCurrentState()
        {
            // Arrange
            _state.ChangeState(UserInterfaceStateType.Goal);

            // Assert
            Assert.AreEqual(UserInterfaceStateType.Goal, _state.State);
        }

        // テストケース3: Resetで状態が 'Normal' に戻ること
        [Test]
        public void Reset_ChangesStateToNormal()
        {
            // Arrange
            _state.ChangeState(UserInterfaceStateType.Stop);

            // Act
            _state.Reset();

            // Assert
            Assert.AreEqual(UserInterfaceStateType.Normal, _state.State);
        }

        // テストケース4: 状態の変更が購読者に通知されること
        [Test]
        public void CurrentState_OnValueChanged_NotifiesSubscriber()
        {
            // Arrange
            var receivedState = UserInterfaceStateType.Normal;
            _state.OnChangeState += pair => receivedState = pair.NextState;

            // Act
            _state.ChangeState(UserInterfaceStateType.Goal);

            // Assert
            Assert.AreEqual(UserInterfaceStateType.Goal, receivedState);
        }
    }
}