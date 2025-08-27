using System;
using GoogleMobileAds.Api;
using Interface.Global.Advertisement;
using R3;
using Structure.Utility.Ads;
using UnityEngine;

namespace View.Global.Advertisement
{
    public class BottomAdsView : IAdvertisementView, IDisposable
    {
        public BottomAdsView(CompositeDisposable disposable)
        {
            disposable.Add(this);
        }

        private BannerView _bannerView;

        public void Dispose()
        {
            _bannerView?.Destroy();
            _bannerView = null;
        }

        public void Spawn()
        {
            _bannerView = new BannerView(
                TestConstants.ADUnitId,
                AdSize.GetLandscapeAnchoredAdaptiveBannerAdSizeWithWidth(Screen.width),
                AdPosition.Bottom
            );
            _bannerView.LoadAd(new AdRequest());
        }

        public void Show()
        {
            _bannerView?.Show();
        }

        public void Hide()
        {
            _bannerView?.Hide();
        }
    }
}