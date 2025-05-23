using Module.StateMachine;
using Structure.InGame.UserInterface;

namespace Adapter.Repository.InGame.Ui
{
    public class UiState : AbstractStateType<UserInterfaceStateType>
    {
        public UiState() : base(UserInterfaceStateType.Normal)
        {
        }
    }
}