using Cysharp.Threading.Tasks;
using Interface.Global.Scene;
using Interface.Global.TimeScale;
using Interface.InGame.UserInterface;
using Module.StateMachine;
using R3;
using Structure.Global.TimeScale;
using Structure.InGame.Player;
using Structure.InGame.UserInterface;
using VContainer.Unity;

namespace Controller.InGame.UserInterface;

/// <summary>
/// 一時停止状態のUIを管理する
/// </summary>
public class StopStateController : UserInterfaceBehaviourBase, IStartable
{
    public StopStateController
    (
        IStopUiView stopUiView,
        IPlayButtonView playButtonView,
        IStop_StageSelectButtonView stageSelectButtonView,
        IStop_RestartButtonView reStartButtonView,
        ILoadPrimarySceneLogic loadPrimarySceneLogic,
        ITimeScaleModel timeScaleModel,
        CompositeDisposable compositeDisposable,
        IMutStateEntity<PlayerStateType> playerState,
        IMutStateEntity<UserInterfaceStateType> stateEntity
    ) : base(UserInterfaceStateType.Stop, stateEntity)
    {
        PlayButtonView = playButtonView;
        StageSelectButtonView = stageSelectButtonView;
        ReStartButtonView = reStartButtonView;
        StopUiView = stopUiView;
        LoadPrimarySceneLogic = loadPrimarySceneLogic;
        TimeScaleModel = timeScaleModel;
        CompositeDisposable = compositeDisposable;
        PlayerState = playerState;
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

    private void Load(string sceneName)
    {
        LoadPrimarySceneLogic.ChangeScene(sceneName).Forget();
    }

    private CompositeDisposable CompositeDisposable { get; }
    private IMutStateEntity<PlayerStateType> PlayerState { get; }
    private IPlayButtonView PlayButtonView { get; }
    private IStopUiView StopUiView { get; }
    private IStop_StageSelectButtonView StageSelectButtonView { get; }
    private IStop_RestartButtonView ReStartButtonView { get; }
    private ILoadPrimarySceneLogic LoadPrimarySceneLogic { get; }
    private ITimeScaleModel TimeScaleModel { get; }
}