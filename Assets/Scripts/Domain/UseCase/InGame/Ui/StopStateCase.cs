using Domain.IPresenter.InGame.UI;
using Domain.IPresenter.Scene;
using Module.StateMachine;
using Structure.InGame.UserInterface;
using VContainer.Unity;

namespace Domain.UseCase.InGame.UI
{
    /// <summary>
    /// 一時停止状態のUIを管理する
    /// </summary>
    public class StopStateCase : UserInterfaceBehaviourBase, IStartable
    {
        public StopStateCase
        (
            IPlayEventPresenter playEventPresenter,
            IStopUiPresenter stopUiPresenter,
            ISceneChangePresenter sceneChangePresenter,
            IScenePresenter scenePresenter,
            IMutStateEntity<UserInterfaceStateType> stateEntity
        ) : base(UserInterfaceStateType.Stop, stateEntity)
        {
            PlayEventPresenter = playEventPresenter;
            StopUiPresenter = stopUiPresenter;
            SceneChangePresenter = sceneChangePresenter;
            ScenePresenter = scenePresenter;
        }
        
        public void Start()
        {
            OnExit();
        }

        public override void OnEnter()
        {
            PlayEventPresenter.PlayEvent += Play;
            SceneChangePresenter.SceneChangeEvent += Load;
            StopUiPresenter.ShowUi();
        }

        public override void OnExit()
        {
            PlayEventPresenter.PlayEvent -= Play;
            SceneChangePresenter.SceneChangeEvent -= Load;
            StopUiPresenter.HideUi();
        }

        private void Play()
        {
            StateEntity.ChangeState(UserInterfaceStateType.Normal);
        }

        private void Load(string sceneName)
        {
            ScenePresenter.Load(sceneName);
        }

        private IPlayEventPresenter PlayEventPresenter { get; }
        private IStopUiPresenter StopUiPresenter { get; }
        private ISceneChangePresenter SceneChangePresenter { get; }
        private IScenePresenter ScenePresenter { get; }
    }
}