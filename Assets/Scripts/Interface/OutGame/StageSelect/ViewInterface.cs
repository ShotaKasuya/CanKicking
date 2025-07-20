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
        Observable<Option<string>> SelectEvent { get; }
    }

    /// <summary>
    /// 選択されたステージを表示する
    /// </summary>
    public interface ISelectedStageView
    {
        public void Reset();
        public void ShowStage(string sceneName);
    }

    /// <summary>
    /// 選択時にシーンを取得できるviewのinterface
    /// </summary>
    public interface ISceneGettableView
    {
        public string Scene { get; }
    }
}