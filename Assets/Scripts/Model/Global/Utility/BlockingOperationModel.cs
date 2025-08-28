using System.Collections.Generic;
using System.Linq;
using System.Text;
using Interface.Model.Global;
using Module.Option.Runtime;
using UnityEngine;

namespace Model.Global.Utility
{
    public class BlockingOperationModel : IBlockingOperationModel
    {
        public BlockingOperationModel
        (
        )
        {
            OperationPool = new OperationPool();
        }

        public OperationHandle SpawnOperation(string context)
        {
            return OperationPool.SpawnOperation(context);
        }

        public bool IsAnyBlocked()
        {
            return !OperationPool.GetOperationHandles.All(x => x.IsEnd);
        }

        public string GetAllOperations()
        {
            return OperationPool.GetAllOperations();
        }

        public IReadOnlyList<OperationHandle> GetOperationHandles => OperationPool.GetOperationHandles;

        private OperationPool OperationPool { get; }
    }
}