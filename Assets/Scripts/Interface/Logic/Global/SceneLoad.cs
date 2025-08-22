using Cysharp.Threading.Tasks;

namespace Interface.Global.Scene;

public interface ILoadPrimarySceneLogic
{
    public UniTask ChangeScene(string scenePath);
}

public interface ILoadSceneResourcesLogic
{
    public UniTask LoadResources();
    public UniTask UnLoadResources();
}
