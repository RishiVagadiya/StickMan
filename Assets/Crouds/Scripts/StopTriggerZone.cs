using UnityEngine;

public class StopTriggerZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player entered Trigger - stopping followers");

            FollowerMovement[] followers = FindObjectsOfType<FollowerMovement>();
            foreach (FollowerMovement follower in followers)
            {
                follower.StopMoving();
            }
        }
    }
}