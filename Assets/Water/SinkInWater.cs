using UnityEngine;
using System.Collections;

public class SinkInWaterWhileMoving : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Followers")) //|| (other.CompareTag("Player")))
        {
            // Start sinking but don't stop movement or animation
            StartCoroutine(SinkWhileMoving(other.gameObject));
        }
    }

    private IEnumerator SinkWhileMoving(GameObject follower)
    {
        float sinkDuration = 2f;
        float elapsed = 0f;
        Vector3 startPosition = follower.transform.position;
        Vector3 endPosition = startPosition + Vector3.down * 2f;

        // Make sure the follower continues forward movement
        Rigidbody rb = follower.GetComponent<Rigidbody>();
        if (rb != null)
        {
            // Keep previous velocity
            // Optional: You can slightly reduce speed if needed by: rb.velocity *= 0.8f;
        }

        // Optional: Just ensure running animation is ON
        Animator anim = follower.GetComponent<Animator>();
        if (anim != null)
        {
            anim.SetBool("Running", true);
        }

        // Smoothly sink down while moving
        while (elapsed < sinkDuration)
        {
            follower.transform.position += Vector3.down * Time.deltaTime; // sink gradually
            elapsed += Time.deltaTime;
            yield return null;
        }

        // Optional: destroy follower after fully drowned
        Destroy(follower);
    }
}
