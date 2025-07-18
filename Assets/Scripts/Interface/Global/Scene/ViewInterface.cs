using Cysharp.Threading.Tasks;
using Module.SceneReference;

namespace Interface.Global.Scene;

/// <summary>
/// 主要シーンを遷移させるイベント
/// </summary>
public interface ISceneSequencerView
{
    public void Sequence(string scenePath);
}

public interface ISceneLoaderView
{
    public UniTask LoadScene(SceneReference sceneContext);
}

/// <summary>
/// シーン読み込みを行うView
/// </summary>
public interface INewSceneLoaderView
{
    public UniTask<SceneReleaseContext> LoadScene(string scenePath);
    public UniTask ActivateAsync(SceneReleaseContext scene);
    public UniTask UnLoadScene(SceneReleaseContext releaseContext);
}