using UnityEngine;

public class ColorFollowerMovement : MonoBehaviour
{
    private Transform target;
    public float followSpeed = 5f;
    public bool isFollowing = false;

    public void SetFollowTarget(Transform newTarget)
{
    target = newTarget;
    isFollowing = true;
}

    void Update()
    {
        if (target != null)
        {
            transform.position = Vector3.Lerp(transform.position, target.position, followSpeed * Time.deltaTime);
        }
    }
}