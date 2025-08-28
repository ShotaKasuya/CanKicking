using System.Collections.Generic;
using Module.Option.Runtime;
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
