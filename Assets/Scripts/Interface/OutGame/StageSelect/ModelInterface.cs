using Module.SceneReference;

namespace Interface.OutGame.StageSelect
{
    /// <summary>
    /// 選択されたステージを保持する
    /// </summary>
    public interface ISelectedStageModel
    {
        public SceneReference SelectedStage { get; }
        public void SetSelectedStage(SceneReference sceneReference);
    }
}