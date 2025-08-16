using UnityEngine;
using TMPro;
using System.IO;

[System.Serializable]
public class UserData
{
    public string username;
    public string password;
    public string mobile;
}

public class AuthUI : MonoBehaviour
{
    [Header("Panels")]
    public GameObject mainPanel;
    public GameObject signUpPanel;
    public GameObject homeLobbyPanel;

    [Header("Signup Inputs")]
    public TMP_InputField signupUsernameInput;
    public TMP_InputField signupPasswordInput;
    public TMP_InputField signupMobileInput;
    public TMP_InputField signupCaptchaInput;
    public TextMeshProUGUI captchaText;

    [Header("Status")]
    public TextMeshProUGUI statusText;

    private string generatedCaptcha;
    private string dataPath;

    void Start()
    {
        mainPanel.SetActive(true);
        signUpPanel.SetActive(false);
        homeLobbyPanel.SetActive(false);
        dataPath = Path.Combine(Application.persistentDataPath, "userData.json");
        GenerateCaptcha();
    }

    // Show SignUp Panel
    public void OnShowSignUpPanel()
    {
        mainPanel.SetActive(false);
        signUpPanel.SetActive(true);
        homeLobbyPanel.SetActive(false);
        GenerateCaptcha();
    }

    // Captcha generate karo
    void GenerateCaptcha()
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        generatedCaptcha = "";
        for (int i = 0; i < 4; i++)
            generatedCaptcha += chars[Random.Range(0, chars.Length)];
        captchaText.text = generatedCaptcha;
    }

    // 🔘 ✅ Sign In Button Logic (Main Button)
    public void OnSignInButtonPressed()
    {
        string username = signupUsernameInput.text.Trim();
        string password = signupPasswordInput.text.Trim();
        string mobile = signupMobileInput.text.Trim();
        string captcha = signupCaptchaInput.text.Trim();

        // 1. Check if any field is empty
        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password) || string.IsNullOrEmpty(mobile) || string.IsNullOrEmpty(captcha))
        {
            statusText.text = "❗ Please input Game Detail in Fields!";
            return;
        }

        // 2. Check captcha
        if (captcha != generatedCaptcha)
        {
            statusText.text = "❌ Wrong Captcha. Try again.";
            GenerateCaptcha();
            return;
        }

        // 3. Create and save JSON
        UserData user = new UserData
        {
            username = username,
            password = password,
            mobile = mobile
        };

        string json = JsonUtility.ToJson(user);
        File.WriteAllText(dataPath, json);

        // 4. Success message + go to Home Lobby
        statusText.text = "✅ Signed in Successfully!";
        signUpPanel.SetActive(false);
        homeLobbyPanel.SetActive(true);
    }
}
