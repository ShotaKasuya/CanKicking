using Module.StateMachine;
using Structure.InGame.Player;
using Structure.InGame.UserInterface;

namespace Tests.Mock
{
    public class MockPlayerStateEntity : AbstractStateType<PlayerStateType>
    {
        public MockPlayerStateEntity(PlayerStateType entryState) : base(entryState)
        {
        }
    }

    public class MockUiStateEntity : AbstractAsyncStateType<UserInterfaceStateType>
    {
        public MockUiStateEntity(UserInterfaceStateType entryState) : base(entryState)
        {
        }
    }
}