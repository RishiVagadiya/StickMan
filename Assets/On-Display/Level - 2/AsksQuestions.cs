using UnityEngine;
using TMPro;

public class AsksQuestions : MonoBehaviour
{
    public GameObject questionPanel; 
    public TextMeshProUGUI questionText; 

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) 
        {
            ShowQuestion();
        }
        else if (other.CompareTag("CorrectAnswer") || other.CompareTag("WrongAnswer")) 
        { 
            questionPanel.gameObject.SetActive(false);
            questionText.gameObject.SetActive(false);
            DisableQPanle();
        }
    }

    void ShowQuestion()
    {
        questionPanel.SetActive(true); 
    }

    void DisableQPanle()
    {
        questionText.gameObject.SetActive(false);
    }
}
