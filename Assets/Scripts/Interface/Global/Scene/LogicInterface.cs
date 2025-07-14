using Cysharp.Threading.Tasks;

namespace Interface.Global.Scene;

public interface ISceneChangeLogic
{
    public UniTask ChangeScene(string scenePath);
}
