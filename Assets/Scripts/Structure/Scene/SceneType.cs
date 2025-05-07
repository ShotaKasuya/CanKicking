namespace DataUtil.Scene
{
    public enum SceneType
    {
        Title,
        StageSelect,
        Stage1,
    }

    public static class SceneTypeExtension
    {
        public static string ToSceneName(this SceneType sceneType)
        {
            return sceneType.ToString();
        }
    }
}