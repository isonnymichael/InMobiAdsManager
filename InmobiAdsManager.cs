using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InMobiAds.Api;

public class InmobiAdsManager : MonoBehaviour
{
    public string appId;
    public string bannerId;
    public string interstitialId;
    public string rewardedVideoId;

    public bool statusAds = true;

    public static InmobiAdsManager Instance;

    private RewardedVideoAd rewardedVideoAd;

    private void Awake()
    {
        if (Instance != null)
            Destroy(gameObject);
        else
            Instance = this;

        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        InMobiPlugin inmobiPlugin = new InMobiPlugin(appId);
    }

    public void RequestBanner()
    {
        var bannerAd = new BannerAd(bannerId, 320, 50, (int)InMobiAdPosition.BottomCenter);
        if (statusAds)
        {
            bannerAd.LoadAd();
        }
        
    }

    public void RequestInterstitial()
    {
        var interstitialAd = new InterstitialAd(interstitialId);
        interstitialAd.LoadAd();
        if (interstitialAd.isReady() && statusAds)
        {
            interstitialAd.Show();
        }
    }

    public void RequestRewardedVideo()
    {
        rewardedVideoAd = new RewardedVideoAd(rewardedVideoId);
        rewardedVideoAd.OnAdRewardActionCompleted += this.HandleRewardActionCompleted;
        rewardedVideoAd.LoadAd();

        if (this.rewardedVideoAd.isReady() && statusAds)
        {
            this.rewardedVideoAd.Show();
        }
    }

    public void HandleRewardActionCompleted(object sender, AdRewardActionCompletedEventArgs args)
    {
       // Give reward here
    }

}
