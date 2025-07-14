using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

namespace Module.SceneReference
{
    public readonly struct SceneContext
    {
        public SceneContext(SceneType sceneType, Scene scene)
        {
            Type = sceneType;
            Scene = scene;
        }

        public SceneType Type { get; }
        public Scene Scene { get; }
    }

    public enum SceneType
    {
        SceneManager,
        Addressable,
    }

    public readonly struct SceneReleaseContext
    {
        public SceneReleaseContext
        (
            SceneType sceneType,
            SceneInstance sceneInstance,
            string sceneName
        )
        {
            Type = sceneType;
            SceneInstance = sceneInstance;
            SceneName = sceneName;
        }
        
        public SceneType Type { get; }
        public SceneInstance SceneInstance { get; }
        public string SceneName { get; }
    }
}