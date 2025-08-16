using GoogleMobileAds.Api;
using GoogleMobileAds.Ump.Api;
using UnityEngine;

public class AppOpenAdManager : MonoBehaviour
{
    private AppOpenAd appOpenAd;
    private string adUnitId = "ca-app-pub-3940256099942544/3419835294"; // test ad unit

    void Start()
    {
        LoadAppOpenAd();
    }

    public void LoadAppOpenAd()
    {
        AdRequest adRequest = new AdRequest();

        AppOpenAd.Load(adUnitId, adRequest, (appOpenAd, error) => {
            if (error != null)
            {
                Debug.Log("Failed to load AppOpenAd: " + error.GetMessage());
                return;
            }

            this.appOpenAd = appOpenAd;
            ShowAppOpenAd();
        });
    }

    public void ShowAppOpenAd()
    {
        if (appOpenAd != null && appOpenAd.CanShowAd())
        {
            appOpenAd.Show();
        }
        else
        {
            Debug.Log("AppOpenAd not ready");
        }
    }
}
