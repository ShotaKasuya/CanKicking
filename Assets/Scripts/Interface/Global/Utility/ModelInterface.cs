using System;
using System.Collections.Generic;
using Module.Option.Runtime;
using UnityEngine;

namespace Interface.Global.Utility;

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

public class OperationHandle : IDisposable
{
    public void Start(string context)
    {
        IsEnd = false;
        OperationContext = context;
    }

    private void Release()
    {
        IsEnd = true;
        OperationContext = string.Empty;
    }

    private string OperationContext { get; set; }
    public bool IsEnd { get; private set; }

    public OperationHandle
    (
    )
    {
        OperationContext = string.Empty;
        IsEnd = true;
    }

    public override string ToString()
    {
        if (IsEnd)
        {
            return "None";
        }

        return $"Some({OperationContext})";
    }

    public void Dispose()
    {
        Release();
    }
}

//====================================================================
// Save Data
//====================================================================

public interface IClearRecordModel
{
    public void Save(string key, int jumpCount);
    public Option<int> Load(string key);
}
