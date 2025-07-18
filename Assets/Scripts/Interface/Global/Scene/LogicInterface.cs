using Cysharp.Threading.Tasks;

namespace Interface.Global.Scene;

public interface ILoadScenePrimaryLogic
{
    public UniTask ChangeScene(string scenePath);
}

public interface ILoadResourcesSceneLogic
{
    public UniTask LoadResources();
    public UniTask UnLoadResources();
}
