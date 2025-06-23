using Module.Option;
using Module.SceneReference;
using R3;

namespace Interface.OutGame.StageSelect
{
    /// <summary>
    /// プレイヤーがステージを選択したイベントを発行する
    /// </summary>
    public interface IStageSelectionView
    {
        Observable<Option<SceneReference>> SelectEvent { get; }
    }
    /// <summary>
    /// 選択されたステージを表示する
    /// </summary>
    public interface ISelectedStageView
    {
        public void Reset();
        public void ShowStage(SceneReference sceneReference);
    }
}