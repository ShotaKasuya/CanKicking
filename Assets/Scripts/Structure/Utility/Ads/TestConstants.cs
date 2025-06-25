namespace Structure.Utility.Ads
{
    public static class TestConstants
    {
        // These ad units are configured to always serve test ads.
#if UNITY_ANDROID
        public const string ADUnitId = "ca-app-pub-3940256099942544/6300978111";
#elif UNITY_IPHONE
        public const string ADUnitId = "ca-app-pub-3940256099942544/2934735716";
#else
        public const string ADUnitId = "unused";
#endif
    }
}