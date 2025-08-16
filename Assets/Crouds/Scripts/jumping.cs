using UnityEngine;

public class jumping : MonoBehaviour
{
    public float jumpForce = 10f;  
    public float forwardForce = 5f;
    private Rigidbody rb;
    private Animator animator;
    private bool isJumping = false;

    public AudioSource jumpSound;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>(); 
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("JumpPad") || other.CompareTag("Player") || other.CompareTag("Followers"))
        {
            Jump();
        }
    }

    void Jump()
    {
        if (!isJumping) 
        {
            isJumping = true;

            animator.SetTrigger("Jump"); 

            if (jumpSound != null)
            {
                jumpSound.Play(); 
            }
     
            rb.linearVelocity = Vector3.zero;       
            rb.AddForce(Vector3.up * jumpForce + Vector3.forward * forwardForce, ForceMode.Impulse);   
            Invoke("Land", 1f); 
        }
    }

    void Land()
    {
        isJumping = false;

        // Resume Running Animation
        animator.SetBool("Running", true); // Resume running
        //animator.SetBool("isRunning", true);
    }
}

