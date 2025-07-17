using Cysharp.Threading.Tasks;

namespace Interface.Global.Scene;

public interface IChangePrimarySceneLogic
{
    public UniTask ChangeScene(string scenePath);
}

public interface ILoadResourcesSceneLogic
{
    public UniTask LoadResources(ISceneResourcesModel sceneResourcesModel);
    public UniTask UnLoadResources(ISceneResourcesModel sceneResourcesModel);
}
