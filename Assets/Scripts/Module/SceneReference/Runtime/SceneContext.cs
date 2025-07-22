using UnityEngine;
using UnityEngine.ResourceManagement.ResourceProviders;

namespace Module.SceneReference
{
    public enum SceneType
    {
        SceneManager,
        Addressable,
    }

    public readonly struct SceneContext
    {
        public SceneContext
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