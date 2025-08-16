using UnityEngine;
using TMPro; // Only if you're using TextMeshPro
using System.Collections.Generic;

public class FollowerNumber : MonoBehaviour
{
    public TextMeshProUGUI numberText; // Use Text if you're using UnityEngine.UI.Text
    [SerializeField] private List<GameObject> allFollowers = new List<GameObject>();

    void Start()
{
    for (int i = 0; i < allFollowers.Count; i++)
    {
        FollowerNumber fn = allFollowers[i].GetComponentInChildren<FollowerNumber>();
        if (fn != null)
        {
            fn.SetNumber(i + 1); // 1-based numbering
        }
    }
}

    public void SetNumber(int number)
    {
        if (numberText != null)
            numberText.text = number.ToString();
    }
}
