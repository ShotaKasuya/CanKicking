using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

#nullable enable

namespace Module.Option.Runtime
{
    public class OperationPool
    {
        public OperationPool
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

        public bool IsAnyBlocked()
        {
            return !GetOperationHandles.All(x => x.IsEnd);
        }

        public IReadOnlyList<OperationHandle> GetOperationHandles => OperationHandles;

        private List<OperationHandle> OperationHandles { get; }
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
}