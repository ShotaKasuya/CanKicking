using Cysharp.Threading.Tasks;
using Module.SceneReference;

namespace Interface.Global.Scene;

/// <summary>
/// シーン読み込みを行うView
/// </summary>
public interface ISceneLoaderView
{
    public UniTask<SceneReleaseContext> LoadScene(string scenePath);
    public UniTask ActivateAsync(SceneReleaseContext scene);
    public UniTask UnLoadScene(SceneReleaseContext releaseContext);
}