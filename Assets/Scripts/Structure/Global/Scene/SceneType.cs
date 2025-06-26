namespace Structure.Scene
{
    public enum SceneType
    {
        Title,
        StageSelect,
        Stage01,
        Stage02,
    }

    public static class SceneTypeExtension
    {
        public static string ToSceneName(this SceneType sceneType)
        {
            return sceneType.ToString();
        }
    }
}