using System.Collections.Generic;

namespace Interface.OutGame.StageSelect;

/// <summary>
/// 選択されたステージを保持する
/// </summary>
public interface ISelectedStageModel
{
    public string SelectedStage { get; }
    public void SetSelectedStage(string scene);
}

public interface IStageScenesModel
{
    public IReadOnlyList<string> SceneList { get; }
}