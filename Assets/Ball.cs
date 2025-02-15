using UnityEngine;

public class Ball : MonoBehaviour
{
    public float minY = -5f;
    public float maxVelocity = 6f;

    private Rigidbody2D rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
    }

    private void Update()
    {
        if(transform.position.y < minY)
        {
            ResetBall();
        }

       
    }

    private void ResetBall()
    {
        transform.position = Vector3.zero;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = 0f;
    }

    private void FixedUpdate()
    {
        if (rb.velocity.magnitude > maxVelocity)
        {
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxVelocity);
        }
    }
}
