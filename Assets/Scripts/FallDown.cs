using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FallDown : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
      if(other.CompareTag("Player") || (other.CompareTag("Followers")))
        {
          SceneManager.LoadScene("FallDown");
        }
    }
     public void RestartCurrentLevel()
     {
       SceneManager.LoadScene("GAME");
     }

     public void QuitGame()
     {
        Application.Quit();
        Debug.Log("Game is Quited !");
     }

}
