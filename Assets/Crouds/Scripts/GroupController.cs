using UnityEngine;
using System.Collections.Generic;

public class GroupController : MonoBehaviour
{
    public Color groupColor;
    public List<Transform> followers = new List<Transform>();
    public bool isMainGroup = false; // सिर्फ main group में true होगा

    public void AddFollower(Transform newFollower)
    {
        followers.Add(newFollower);
        // Optional: Animation ya formation set karo yahan
        newFollower.GetComponent<ColorFollowerMovement>().SetFollowTarget(transform);
    }
}
