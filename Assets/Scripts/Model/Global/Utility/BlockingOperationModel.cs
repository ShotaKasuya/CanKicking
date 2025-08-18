using System.Collections.Generic;
using System.Linq;
using System.Text;
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
            var logBuilder = new StringBuilder();
            logBuilder.Append("operation overflow\n");
            for (int i = 0; i < OperationHandles.Count; i++)
            {
                logBuilder.Append(OperationHandles[i]);
            }

            Debug.LogWarning(logBuilder.ToString());
#endif

            var handle = new OperationHandle();
            OperationHandles.Add(handle);
            return handle;
        }

        public bool IsAnyBlocked()
        {
            return !OperationHandles.All(x => x.IsEnd);
        }

        public string GetAllOperations()
        {
            var operations = new StringBuilder();
            for (int i = 0; i < OperationHandles.Count; i++)
            {
                operations.Append(OperationHandles[i]);
                operations.Append("\n");
            }

            return operations.ToString();
        }

        public IReadOnlyList<OperationHandle> GetOperationHandles => OperationHandles;

        private List<OperationHandle> OperationHandles { get; }
    }
}