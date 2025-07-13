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
}