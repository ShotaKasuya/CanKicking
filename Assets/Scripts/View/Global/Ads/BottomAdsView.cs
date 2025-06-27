using System;
using GoogleMobileAds.Api;
using Structure.Utility.Ads;
using UnityEngine;
using VContainer.Unity;

namespace View.Global.Ads
{
    public class BottomAdsView : IStartable, IDisposable
    {
        public void Start()
        {
            MobileAds.Initialize((status =>
            {
                if (status == null)
                {
                    Debug.LogError("Google Mobile Ads Initialization Failed.");
                }

                Debug.Log("Google Mobile Ads Initialization Complete.");
            }));
            _bannerView = new BannerView(
                TestConstants.ADUnitId,
                AdSize.GetLandscapeAnchoredAdaptiveBannerAdSizeWithWidth(Screen.width),
                AdPosition.Bottom
            );
            _bannerView.LoadAd(new AdRequest());
        }

        private BannerView _bannerView;

        public void Dispose()
        {
            _bannerView.Destroy();
            _bannerView = null;
        }
    }
}