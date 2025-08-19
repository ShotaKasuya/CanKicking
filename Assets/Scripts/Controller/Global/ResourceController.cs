using GoogleMobileAds.Api;
using UnityEngine;
using VContainer.Unity;

namespace Controller.Global;

/// <summary>
/// 静的リソース関連の処理を持つ
/// </summary>
public class ResourceController: IInitializable
{
    public void Initialize()
    {
#if UNITY_ANDROID || UNITY_IOS
        MobileAds.Initialize(status =>
        {
            if (status == null)
            {
                Debug.LogError("Google Mobile Ads Initialization Failed.");
            }

            Debug.Log("Google Mobile Ads Initialization Complete.");
        });
#endif
    }
}