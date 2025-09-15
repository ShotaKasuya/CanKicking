using System.Collections.Generic;
using Interface.Model.Global;
using Module.Option.Runtime;
using UnityEngine;

namespace Tests.Mock.Global
{
    public class MockScreenScaleModel : IScreenScaleModel
    {
        public Vector2 Scale { get; set; } = new(1080, 1920);
        public float Width => Scale.x;
        public float Height => Scale.y;
    }

    public class MockBlockingOperationModel : IBlockingOperationModel
    {
        public int SpawnCount { get; private set; }
        public OperationHandle SpawnOperation(string context) { SpawnCount++; return new OperationHandle(); }
        public bool IsAnyBlocked() => false;
        public IReadOnlyList<OperationHandle> GetOperationHandles => null;
    }
}