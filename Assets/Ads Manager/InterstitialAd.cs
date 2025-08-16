using UnityEngine;
using UnityEngine.Advertisements;

public class InterstitialAd : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [SerializeField] private string androidAdUnitId = "Interstitial_Android";

    private bool adIsLoaded = false;

    void Start()
    {
        Advertisement.Initialize("5838022", true); // Replace with your real Game ID
        LoadAd();
    }

    public void LoadAd()
    {
        Advertisement.Load(androidAdUnitId, this);
        Debug.Log("Unity ADs Loaded Succeddfully !");
    }

    public void ShowAd()
    {
        if (adIsLoaded)
        {
            Advertisement.Show(androidAdUnitId, this);
            Debug.Log("Unity Ads Is Showing");
            adIsLoaded = false; // Reset
        }
        else
        {
            Debug.Log("Ad not ready yet.");
        }
    }

    // Called when ad is loaded
    public void OnUnityAdsAdLoaded(string placementId)
    {
        if (placementId == androidAdUnitId)
        {
            Debug.Log("Ad successfully loaded.");
            adIsLoaded = true;
        }
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        Debug.LogError($"Ad Failed to load: {placementId} - {error.ToString()} - {message}");
    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        Debug.Log("Ad show complete.");
        LoadAd(); // Reload after show
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        Debug.LogError($"Ad show failed: {placementId} - {error.ToString()} - {message}");
    }

    public void OnUnityAdsShowStart(string placementId) { }

    public void OnUnityAdsShowClick(string placementId) { }
}
