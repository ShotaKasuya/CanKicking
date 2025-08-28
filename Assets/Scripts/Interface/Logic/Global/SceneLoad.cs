using Cysharp.Threading.Tasks;

namespace Interface.Logic.Global;

public interface ILoadPrimarySceneLogic
{
    public UniTask ChangeScene(string scenePath);
}

public interface ILoadSceneResourcesLogic
{
    public UniTask LoadResources();
    public UniTask UnLoadResources();
}
