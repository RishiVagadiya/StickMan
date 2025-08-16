using UnityEngine;
using System.Collections.Generic;

public class GroupJoinTrigger : MonoBehaviour
{
    public Transform standingGroupParent; // This group's followers (standing group)
    public bool hasJoined = false;
    public FollowerManager followerManager;

    public void OnTriggerEnter(Collider other)
    {
        if (hasJoined) return;

        if (other.CompareTag("Player"))
        {
            Debug.Log("✅ Main Player triggered the GroupJoinTrigger");

            MeshRenderer myRenderer = GetComponentInChildren<MeshRenderer>();
            MeshRenderer otherRenderer = other.GetComponentInChildren<MeshRenderer>();

            if (myRenderer == null || otherRenderer == null)
            {
                Debug.LogWarning("❌ MeshRenderer missing on group or player.");
                return;
            }

            Color myColor = myRenderer.material.color;
            Color otherColor = otherRenderer.material.color;

            Debug.Log($"🎨 My Group Color: {myColor} | Player Color: {otherColor}");

            if (AreColorsEqual(myColor, otherColor))
            {
                Debug.Log("✅ Color matched — Group will now join and move with Player");
                JoinGroup(other.transform);
                hasJoined = true;
            }
            else
            {
                Debug.Log("❌ Color did not match — Group will not join");
            }
        }
    }

    void JoinGroup(Transform mainGroupTransform)
    {
        if (followerManager != null)
        {
            foreach (Transform follower in standingGroupParent)
            {
                // ✅ Move follower under player's group
                follower.SetParent(mainGroupTransform);

                // ✅ Add to followerManager list
                if (!followerManager.followers.Contains(follower))
                {
                    followerManager.followers.Add(follower);
                    Debug.Log($"🟢 Follower Added: {follower.name}");
                }

                // ✅ Start movement
                FollowerMovement moveScript = follower.GetComponent<FollowerMovement>();
                if (moveScript != null)
                {
                    moveScript.StartMoving();
                    Debug.Log($"🏃 Started Moving: {follower.name}");
                }
                else
                {
                    Debug.LogWarning($"⚠️ No FollowerMovement script on: {follower.name}");
                }
            }
        }
        else
        {
            Debug.LogError("❌ FollowerManager reference is missing!");
        }
    }

    bool AreColorsEqual(Color a, Color b, float tolerance = 0.01f)
    {
        return Mathf.Abs(a.r - b.r) < tolerance &&
               Mathf.Abs(a.g - b.g) < tolerance &&
               Mathf.Abs(a.b - b.b) < tolerance;
    }
}
