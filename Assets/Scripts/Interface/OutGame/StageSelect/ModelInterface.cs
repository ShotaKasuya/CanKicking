namespace Interface.OutGame.StageSelect;

/// <summary>
/// 選択されたステージを保持する
/// </summary>
public interface ISelectedStageModel
{
    public string SelectedStage { get; }
    public void SetSelectedStage(string scene);
}