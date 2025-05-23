using Domain.IPresenter.InGame.UI;
using Domain.IPresenter.Scene;
using Module.StateMachine;
using Structure.InGame.UserInterface;

namespace Domain.UseCase.InGame.UI
{
    /// <summary>
    /// ゴール到達後のUIを管理する
    /// </summary>
    public class GoalStateCase : UserInterfaceBehaviourBase
    {
        public GoalStateCase
        (
            IGoalUiPresenter goalUiPresenter,
            ISceneChangePresenter sceneChangePresenter,
            IScenePresenter scenePresenter,
            IMutStateEntity<UserInterfaceStateType> stateEntity
        ) : base(UserInterfaceStateType.Goal, stateEntity)
        {
            GoalUiPresenter = goalUiPresenter;
            SceneChangePresenter = sceneChangePresenter;
            ScenePresenter = scenePresenter;
        }

        public override void OnEnter()
        {
            SceneChangePresenter.SceneChangeEvent += Load;
            GoalUiPresenter.ShowUi();
        }

        private void Load(string sceneName)
        {
            ScenePresenter.Load(sceneName);
        }
        
        private IGoalUiPresenter GoalUiPresenter { get; }
        private ISceneChangePresenter SceneChangePresenter { get; }
        private IScenePresenter ScenePresenter { get; }
    }
}