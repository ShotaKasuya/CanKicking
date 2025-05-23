using Module.StateMachine;
using Structure.InGame.UserInterface;

namespace Domain.IRepository.InGame.Ui
{
    public interface IUiStateRepository : IState<UserInterfaceStateType>
    {
    }

    public interface IMutUiStateRepository : IMutStateEntity<UserInterfaceStateType>
    {
    }
}