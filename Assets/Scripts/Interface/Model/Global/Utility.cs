using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Module.Option.Runtime;
using Structure.Global;
using UnityEngine;

namespace Interface.Model.Global;

//====================================================================
// Screen Scale
//====================================================================
public interface IScreenScaleModel
{
    public Vector2 Scale { get; }
    public float Width { get; }
    public float Height { get; }
}

//====================================================================
// Blocking Operation
//====================================================================
public interface IBlockingOperationModel
{
    public OperationHandle SpawnOperation(string context);
    public bool IsAnyBlocked();
    public IReadOnlyList<OperationHandle> GetOperationHandles { get; }
}

//====================================================================
// Save Data
//====================================================================

public interface IClearRecordModel
{
    public void Save(string key, int jumpCount);
    public Option<int> Load(string key);
}

public interface IRepositoryFlushModel
{
    public UniTask Flush();
}

//====================================================================
// Primary Data
//====================================================================

public interface IGameStateModel
{
    public UniTask Initialize();
    public void UpdateGameState(GameState gameState);
    public GameState GameState { get; }

    /// <summary>
    /// `PlayerState`が`StageCleared`の場合にクリアしたステージが保存される
    /// </summary>
    public string ClearedStageName { get; }

    public Dictionary<string, StageProgressData> ProgressData { get; }
}