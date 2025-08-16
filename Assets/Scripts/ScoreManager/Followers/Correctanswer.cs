using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Correctanswer : MonoBehaviour
{
    public AudioClip correctSound;
    private AudioSource audioSource;
   
    

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
        }
    }
    // **Remove only the enabled players from the list**
    //disabledBluePlayers.RemoveRange(0, enableCount);
}

