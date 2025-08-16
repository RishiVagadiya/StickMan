using UnityEngine;
using UnityEngine.UI;

public class InternetManager : MonoBehaviour
{
    // Singleton instance
    private static InternetManager _instance;
    public static InternetManager Instance
    {
        get
        {
            if (_instance == null)
            {
                GameObject go = new GameObject("InternetManager");
                _instance = go.AddComponent<InternetManager>();
                DontDestroyOnLoad(go);
            }
            return _instance;
        }
    }

    [Header("UI Elements")]
    [SerializeField] private GameObject internetPanel;    // Internet status panel
    [SerializeField] private GameObject wifiGreenIcon;    // Green WiFi icon GameObject
    [SerializeField] private Image wifiIcon;             // Main WiFi icon
    [SerializeField] private Sprite wifiGreenSprite;     // Green sprite
    [SerializeField] private Sprite wifiRedSprite;       // Red sprite

    private BackgroundMusicController musicController;
    private bool isPaused = false;

    private void Awake()
    {
        // Ensure only one instance exists
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        _instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        // Initialize UI elements if not assigned
        if (wifiGreenIcon == null)
        {
            wifiGreenIcon = new GameObject("WifiGreenIcon");
            wifiGreenIcon.transform.SetParent(transform);
            wifiGreenIcon.SetActive(false);
        }

        InvokeRepeating("CheckInternetConnection", 0f, 5f); // Check every 5 seconds
    }

    void CheckInternetConnection()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            Debug.Log("No Internet Connection Detected");
            HandleNoInternet();
        }
        else
        {
            Debug.Log("Internet Connection Available");
            HandleInternetAvailable();
        }
    }

    private void HandleNoInternet()
    {
        wifiIcon.sprite = wifiRedSprite;
        wifiGreenIcon.SetActive(false);    // Hide green icon
        internetPanel.SetActive(true);
        PauseGame();
    }

    private void HandleInternetAvailable()
    {
        wifiIcon.sprite = wifiGreenSprite;
        wifiGreenIcon.SetActive(true);     // Show green icon
        internetPanel.SetActive(false);
        ResumeGame();
    }

    void PauseGame()
    {
        if (!isPaused)
        {
            Time.timeScale = 0f;
            isPaused = true;
            if (musicController != null)
            {
                musicController.StopMusic();
            }
        }
    }

    void ResumeGame()
    {
        if (isPaused)
        {
            Time.timeScale = 1f;
            isPaused = false;
            if (musicController != null)
            {
                musicController.PlayMusic();
            }
        }
    }

    public void RetryConnection()
    {
        Debug.Log("Retrying Internet Connection...");
        CheckInternetConnection();
    }

    // Method to set music controller reference
    public void SetMusicController(BackgroundMusicController controller)
    {
        musicController = controller;
    }
}