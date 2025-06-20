using GoogleMobileAds.Api;
using Structure.Util.Ads;
using UnityEngine;

namespace Adapter.View.Util
{
    public class AdsView: MonoBehaviour
    {
        private BannerView _bannerView;
        
        public void Start()
        {
            _bannerView = new BannerView(TestConstants.ADUnitId, AdSize.GetLandscapeAnchoredAdaptiveBannerAdSizeWithWidth(Screen.width), AdPosition.Bottom);
            _bannerView.LoadAd(new AdRequest());
        }

        private void OnDestroy()
        {
            _bannerView?.Destroy();
        }
    }
}