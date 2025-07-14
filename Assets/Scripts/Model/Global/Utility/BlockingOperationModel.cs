using System.Collections.Generic;
using System.Linq;
using Interface.Global.Utility;
using UnityEngine;

namespace Model.Global.Utility
{
    public class BlockingOperationModel : IBlockingOperationModel
    {
        public BlockingOperationModel
        (
        )
        {
            const int handleLength = 8;

            OperationHandles = new List<OperationHandle>(handleLength);

            for (int i = 0; i < handleLength; i++)
            {
                var handle = new OperationHandle();
                OperationHandles.Add(handle);
            }
        }

        public OperationHandle SpawnOperation(string context)
        {
            for (int i = 0; i < OperationHandles.Count; i++)
            {
                if (OperationHandles[i].IsEnd)
                {
                    OperationHandles[i].Start(context);
                    return OperationHandles[i];
                }
            }

#if UNITY_EDITOR
            var operations = string.Empty;
            for (int i = 0; i < OperationHandles.Count; i++)
            {
                operations += OperationHandles[i].ToString();
            }

            Debug.LogWarning("operation overflow\n" + operations);
#endif

            var handle = new OperationHandle();
            OperationHandles.Add(handle);
            return handle;
        }

        public bool IsAnyBlocked()
        {
            return OperationHandles.Any(x => x.IsEnd);
        }

        private List<OperationHandle> OperationHandles { get; }
    }
}