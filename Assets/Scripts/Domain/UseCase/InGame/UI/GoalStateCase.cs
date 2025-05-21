using Domain.IPresenter.InGame.UI;
using Domain.IPresenter.Scene;
using Domain.IUseCase.InGame;
using Module.StateMachine;
using Structure.InGame.UserInterface;

namespace Domain.UseCase.InGame.UI
{
    public class GoalStateCase : UserInterfaceBehaviourBase
    {
        public GoalStateCase
        (
            ISceneChangePresenter sceneChangePresenter,
            IGoalPresenter goalPresenter,
            IScenePresenter scenePresenter,
            IMutStateEntity<UserInterfaceStateType> stateEntity
        ) : base(UserInterfaceStateType.Goal, stateEntity)
        {
            SceneChangePresenter = sceneChangePresenter;
            GoalPresenter = goalPresenter;
            ScenePresenter = scenePresenter;

            SceneChangePresenter.SceneChangeEvent += Load;
        }

        public override void OnEnter()
        {
            GoalPresenter.Goal(new GoalArg());
        }

        private void Load(string sceneName)
        {
            ScenePresenter.Load(sceneName);
        }
        
        private IGoalPresenter GoalPresenter { get; }
        private ISceneChangePresenter SceneChangePresenter { get; }
        private IScenePresenter ScenePresenter { get; }
    }
}