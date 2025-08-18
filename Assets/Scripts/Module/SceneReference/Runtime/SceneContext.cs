using UnityEngine;
using UnityEngine.ResourceManagement.ResourceProviders;

namespace Module.SceneReference.Runtime
{
    public enum SceneType
    {
        SceneManager,
        Addressable,
    }

    public readonly struct SceneContext
    {
        public static SceneContext SceneManagerContext(AsyncOperation operation, string scenePath)
        {
            return new SceneContext(SceneType.SceneManager, default, operation, scenePath);
        }

        public static SceneContext AddressableContext(SceneInstance sceneInstance, string scenePath)
        {
            return new SceneContext(SceneType.Addressable, sceneInstance, null, scenePath);
        }

        private SceneContext
        (
            SceneType sceneType,
            SceneInstance sceneInstance,
            AsyncOperation operation,
            string scenePath
        )
        {
            Type = sceneType;
            SceneInstance = sceneInstance;
            Operation = operation;
            ScenePath = scenePath;
        }

        public SceneType Type { get; }
        public SceneInstance SceneInstance { get; }
        public AsyncOperation Operation { get; }
        public string ScenePath { get; }
    }
}