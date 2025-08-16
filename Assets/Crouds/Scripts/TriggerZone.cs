using UnityEngine;

public class TriggerZone : MonoBehaviour
{
    public FollowerManager followerManager;
    public FollowerManager thirdfollowermanager;
    public FollowerUIManager uiManager; // ✅ UI Manager भी assign करना होगा
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            followerManager.TriggerFormation();

            foreach (Transform follower in followerManager.followers)
            {
                FollowerMovement moveScript = follower.GetComponent<FollowerMovement>();
                if (moveScript != null)
                {
                    moveScript.StartMoving();
                }
            }

            // ✅ Followers की संख्या अब जोड़ो
            uiManager.AddFollowers(followerManager.followers.Count);
        }
    }
}
