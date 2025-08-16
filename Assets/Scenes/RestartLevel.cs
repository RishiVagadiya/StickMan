using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;

public class RestartLevel : MonoBehaviour
{
    private string savePath;

    void Start()
    {
        savePath = Application.persistentDataPath + "/gameData.json";
    }

    public void RestartCurrentLevel()
    {
        DeleteSavedData(); // JSON फाइल डिलीट करो
        SceneManager.LoadScene("GAME"); // मुख्य गेम सीन को रीलोड करो
    }

    private void DeleteSavedData()
    {
        if (File.Exists(savePath))
        {
            File.Delete(savePath);
            Debug.Log("Saved Game Data Deleted!");
        }
    }
}
