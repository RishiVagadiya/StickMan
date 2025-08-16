using System.Collections.Generic;
using UnityEngine;

public class FollowerManager : MonoBehaviour
{
    public List<Transform> followers = new List<Transform>();
    public Transform centerPoint;
    public float radius = 2f;
    public float moveSpeed = 3f;
    public GameObject finishLineStop;
    private bool arrangeCircle = false;
    private bool stopwhenfinished = false;
    public bool allowArrange = true;
    public bool stopAllArrangement = false;

    public GameObject followersParent;

    void Update()
    {
        if (arrangeCircle && allowArrange && !stopAllArrangement)
        {
            ArrangeInCircleSmooth();
        }
    }

    public void ArrangeInCircleSmooth()
    {
        int count = followers.Count;

        for (int i = 0; i < count; i++)
        {
            float angle = i * Mathf.PI * 2f / count;
            Vector3 targetPosition = centerPoint.position + new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * radius;
            followers[i].position = Vector3.Lerp(followers[i].position, targetPosition, Time.deltaTime * moveSpeed);
        }
    }

    public void TriggerFormation()
    {
        if (allowArrange)
        {
            arrangeCircle = true;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") || other.CompareTag("Followers"))
        {

            BoxCollider box = finishLineStop.GetComponent<BoxCollider>();
            if (box != null)
            {
                box.isTrigger = true;
            }

            foreach (Transform follower in followersParent.transform)
            {
                FollowerMovement moveScript = follower.GetComponent<FollowerMovement>();
                if (moveScript != null)
                {
                    moveScript.StopMoving();
                }
            }
        }

        if (other.CompareTag("Follower"))
        {

            BoxCollider box = finishLineStop.GetComponent<BoxCollider>();
            if (box != null)
            {
                box.isTrigger = false;
            }

            stopwhenfinished = true;
        }
    }

    public void StopAllArrangementsAfterStack()
    {
        stopAllArrangement = true;
    }
}
