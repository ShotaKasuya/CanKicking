using Cysharp.Threading.Tasks;
using Module.SceneReference;

namespace Interface.Global.Scene;

/// <summary>
/// シーン読み込みを行うView
/// </summary>
public interface ISceneLoaderView
{
    public UniTask<SceneContext> LoadScene(string scenePath);
    public UniTask ActivateAsync(SceneContext scene);
    public void SetActiveScene(SceneContext scene);
    public UniTask UnLoadScene(SceneContext context);
    
    public UnityEngine.SceneManagement.Scene CurrentScene { get; }
}