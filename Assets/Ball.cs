using UnityEngine;

public class Ball : MonoBehaviour
{
    public float minY = -5f;
    public float maxVelocity = 6f;
    public bool canMove = false;

    public GameManager gameManager;
    public AudioClip hitClip;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb.isKinematic = true;
    }

    private void Update()
    {
        if (!gameManager.hasGameStarted)
        {
            rb.velocity = Vector3.zero; // Ensure ball doesn't move if the game is over
            spriteRenderer.enabled = false; // Hide ball sprite when game is over
            return;
        }

        spriteRenderer.enabled = true; // Enable sprite when game starts

        if (!canMove) return;

        rb.isKinematic = false;

        if (transform.position.y < minY)
        {
            gameManager.RemoveScore(5f);
            ResetBall();
        }
    }

    private void ResetBall()
    {
        transform.position = Vector3.zero;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = 0f;
        canMove = false;
        spriteRenderer.enabled = false; // Hide ball sprite on reset

        // Restart the ball after a short delay
        Invoke(nameof(EnableMovement), 1f);
    }

    private void EnableMovement()
    {
        if (gameManager.hasGameStarted)
        {
            canMove = true;
            spriteRenderer.enabled = true; // Show ball sprite when movement starts
        }
    }

    private void FixedUpdate()
    {
        if (rb.velocity.magnitude > maxVelocity)
        {
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxVelocity);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Brick"))
        {
            gameManager.AddScore(5f);
            PlaySound(hitClip);
            Destroy(collision.gameObject);
        }
    }

    private void PlaySound(AudioClip clip)
    {
        if (clip == null) return;

        GameObject pickupAudio = new GameObject("AudioPickup");
        AudioSource pickupAudioSource = pickupAudio.AddComponent<AudioSource>();
        pickupAudioSource.volume = 0.4f;
        pickupAudioSource.clip = clip;
        pickupAudioSource.Play();
        Destroy(pickupAudio, clip.length);
    }
}
