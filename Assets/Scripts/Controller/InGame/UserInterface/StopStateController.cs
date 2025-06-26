using System;
using Interface.Global.Scene;
using Interface.Global.TimeScale;
using Interface.InGame.UserInterface;
using Module.SceneReference;
using Module.StateMachine;
using R3;
using Structure.Global.TimeScale;
using Structure.InGame.Player;
using Structure.InGame.UserInterface;
using VContainer.Unity;

namespace Controller.InGame.UserInterface
{
    /// <summary>
    /// 一時停止状態のUIを管理する
    /// </summary>
    public class StopStateController : UserInterfaceBehaviourBase, IStartable, IDisposable
    {
        /// <summary>
        /// 一時停止状態のUIを管理する
        /// </summary>
        public StopStateController
        (
            IStopUiView stopUiView,
            IPlayButtonView playButtonView,
            IStop_StageSelectButtonView stageSelectButtonView,
            IStop_RestartButtonView reStartButtonView,
            // IScreenScaleSliderView screenScaleSliderView,
            ISceneLoaderView sceneLoaderView,
            ITimeScaleModel timeScaleModel,
            IMutStateEntity<PlayerStateType> playerState,
            IMutStateEntity<UserInterfaceStateType> stateEntity
        ) : base(UserInterfaceStateType.Stop, stateEntity)
        {
            PlayButtonView = playButtonView;
            StageSelectButtonView = stageSelectButtonView;
            ReStartButtonView = reStartButtonView;
            StopUiView = stopUiView;
            // ScreenScaleSliderView = screenScaleSliderView;
            SceneLoaderView = sceneLoaderView;
            TimeScaleModel = timeScaleModel;
            PlayerState = playerState;
        
            CompositeDisposable = new CompositeDisposable();
        }
        
        public void Start()
        {
            OnExit();
            PlayButtonView.Performed
                .Where(this, (_, controller) => controller.IsInState())
                .Subscribe(this, (_, controller) => controller.Play())
                .AddTo(CompositeDisposable);
            StageSelectButtonView.Performed
                .Where(this, (_, controller) => controller.IsInState())
                .Subscribe(this, (scene, controller) => controller.Load(scene))
                .AddTo(CompositeDisposable);
            ReStartButtonView.Performed
                .Where(this, (_, controller) => controller.IsInState())
                .Subscribe(this, (scene, controller) => controller.Load(scene))
                .AddTo(CompositeDisposable);
            // todo: 描画範囲変更
            // ScreenScaleSliderView.ChangeObservable
            //     .Where(_ => IsInState())
            //     .Subscribe(ScreenWidthRepository.SetWeight)
            //     .AddTo(CompositeDisposable);
        }
        
        public override void OnEnter()
        {
            PlayerState.ChangeState(PlayerStateType.Stopping);
            TimeScaleModel.Execute(TimeCommandType.Stop);
            StopUiView.Show();
        }
        
        public override void OnExit()
        {
            PlayerState.ChangeState(PlayerStateType.Idle);
            TimeScaleModel.Undo();
            StopUiView.Hide();
        }
        
        private void Play()
        {
            StateEntity.ChangeState(UserInterfaceStateType.Normal);
        }
        
        private void Load(SceneReference sceneReference)
        {
            SceneLoaderView.LoadScene(sceneReference);
        }
        
        private CompositeDisposable CompositeDisposable { get; }
        private IMutStateEntity<PlayerStateType> PlayerState { get; }
        private IPlayButtonView PlayButtonView { get; }
        private IStopUiView StopUiView { get; }
        private IStop_StageSelectButtonView StageSelectButtonView { get; }
        private IStop_RestartButtonView ReStartButtonView { get; }
        // private IScreenScaleSliderView ScreenScaleSliderView { get; }
        private ISceneLoaderView SceneLoaderView { get; }
        private ITimeScaleModel TimeScaleModel { get; }
        
        public void Dispose()
        {
            CompositeDisposable?.Dispose();
        }
    }
}