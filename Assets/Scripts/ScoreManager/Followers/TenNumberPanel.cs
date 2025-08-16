using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class TenNumberPanel : MonoBehaviour
{
    public AudioClip correctSound;
    private AudioSource audioSource;
    private List<GameObject> disabledBluePlayers = new List<GameObject>(); // Disable हुए players को स्टोर करने के लिए लिस्ट

    public int panelNumber; // **Inspector में Set करने के लिए Panel Number**

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Player का Tag चेक करें
        {
            ScoreManager.instance.AddScore(10);

            // Sound Play करें
            if (correctSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(correctSound);
            }

            // **Panel के number के अनुसार players को enable करना**
            EnableBluePlayers(panelNumber);
        }
    }

    public void DisableBluePlayer(GameObject player)  // जब player disable हो तो उसे लिस्ट में add करें
   {
    if (!disabledBluePlayers.Contains(player))
    {
        disabledBluePlayers.Add(player);    
        Debug.Log("🛑 Added to disabled list: " + player.name);
    }
    else
    {
        Debug.LogWarning("⚠️ Already in disabled list: " + player.name);
    }

    player.SetActive(false);
    Debug.Log("🔻 Total Disabled Players: " + disabledBluePlayers.Count);
}

    void EnableBluePlayers(int count)
    {
    int available = disabledBluePlayers.Count;
    if (available == 0)
    {
        Debug.LogWarning("❌ No disabled players available to enable!");
        return;
    }

    int enableCount = Mathf.Min(count, available);
    Debug.Log("🔹 Enabling " + enableCount + " Disabled Blue Players. Total Available: " + available);

    for (int i = 0; i < enableCount; i++)
    {
        if (disabledBluePlayers[i] != null)
        {
            disabledBluePlayers[i].SetActive(true);
            Debug.Log("✅ Enabled: " + disabledBluePlayers[i].name);
        }
    }

    // **Remove only the enabled players from the list**
    //disabledBluePlayers.RemoveRange(0, enableCount);
}
}
