using UnityEngine;

public class FollowerMovement : MonoBehaviour
{
    public float moveSpeed = 3f;
    private bool canMove = false;
    private Rigidbody rb;
    private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        if (rb != null && !rb.isKinematic)
        {
            rb.linearVelocity = Vector3.zero;
        }
        canMove = false;

        if (animator != null)
            animator.SetBool("Running", false);
    }

    void Update()
    {
        if (canMove)
        {
            if (rb != null && !rb.isKinematic)
            {
                rb.linearVelocity = transform.forward * moveSpeed;
            }
            else
            {
                transform.position += transform.forward * moveSpeed * Time.deltaTime;
            }

            if (animator != null)
                animator.SetBool("Running", true);
        }
        else
        {
            if (rb != null && !rb.isKinematic)
            {
                rb.linearVelocity = Vector3.zero;
            }

            if (animator != null)
                animator.SetBool("Running", false);
        }
    }

    public void StartMoving()
    {
        canMove = true;
        if (animator != null)
            animator.SetBool("Running", true);
    }

    public void StopMoving()
    {
        canMove = false;
        
        if (rb != null && !rb.isKinematic)
        {
            rb.linearVelocity = Vector3.zero;
        }

        if (animator != null)
            animator.SetBool("Running", false);
    }
}
