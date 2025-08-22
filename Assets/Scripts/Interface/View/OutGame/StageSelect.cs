using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Module.Option;
using Module.Option.Runtime;
using R3;
using UnityEngine;

namespace Interface.OutGame.StageSelect;

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
    public void ShowStage(string sceneName, Option<int> clearRecord);
}

/// <summary>
/// 選択時にシーンを取得できるviewのinterface
/// </summary>
public interface ISceneGettableView
{
    public string Scene { get; }
}

/// <summary>
/// ステージ選択を行うアイコンを作成する
/// </summary>
public interface IStageIconFactoryView
{
    public UniTask MakeIcons(IReadOnlyList<string> sceneNames);
}

//===================================================================
// Camera
//===================================================================

public interface ICameraPositionView
{
    public void AddForce(Vector2 vector2);
}
