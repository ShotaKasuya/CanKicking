using System.Collections.Generic;
using Interface.Model.Global;
using Module.Option.Runtime;

namespace Tests.Mock.Controller.Global.Scene
{
    public class MockBlockingOperationModel : IBlockingOperationModel
    {
        public int SpawnCount { get; private set; }

        public OperationHandle SpawnOperation(string context)
        {
            SpawnCount++;
            return new OperationHandle();
        }

        public bool IsAnyBlocked() => false;
        public IReadOnlyList<OperationHandle> GetOperationHandles => null;
    }
}
