using Module.SceneReference;

namespace Interface.OutGame.Title
{
    /// <summary>
    /// ゲーム開始時のシーン情報を提供する。
    /// </summary>
    public interface IGameStartSceneModel
    {
        public SceneReference GetStartScene();
    }
}