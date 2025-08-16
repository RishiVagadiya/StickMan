using UnityEngine;
using TMPro;

public class FollowerUIManager : MonoBehaviour
{
    public TextMeshProUGUI followersText;
    private int currentFollowersCount = 0;

    public void AddFollowers(int count)
    {
        currentFollowersCount += count;
        UpdateUI();
    }

    void UpdateUI()
    {
        followersText.text = currentFollowersCount.ToString();
    }
}
