using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] GameManager gameManger;
    [SerializeField] Paddle paddle;

    [SerializeField] float scoreValue; // good pickup will add this score, bad will deduct
    [SerializeField] float paddleSizeValue; // same, good = add, bad = subtract

    [SerializeField] float maxPaddleSize;
    [SerializeField] float minPaddleSize;

    [SerializeField] AudioSource goodSound;
    [SerializeField] AudioSource badSound;

    private float screenLimitY;

    private void Start()
    {
        // find the bottom of the screen
        screenLimitY = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, Camera.main.transform.position.z)).y;
    }


    private void Update()
    {
        // disable the pickup if it falls off the bottom of the screen
        if ( transform.position.y < screenLimitY )
        {
            gameObject.SetActive(false);
        }
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ( collision.gameObject.CompareTag("Player"))
        {
            if ( Random.Range(0,100) <= 50 ) // 50/50 chance of being good or bad
            {
                DoBadPickup();
            }
            else
            {
                DoGoodPickup();
            }

            gameObject.SetActive(false); // disable us so we can be ready to respawn again
        }
    }

    void DoBadPickup()
    {
        gameManger.AddScore(-scoreValue);

        if ( paddle.transform.localScale.x > (minPaddleSize + paddleSizeValue) )
            paddle.transform.localScale += new Vector3(-paddleSizeValue, 0, 0);

        else
            paddle.transform.localScale = new Vector3(minPaddleSize, paddle.transform.localScale.y, paddle.transform.localScale.z);

        badSound.Play();
    }

    void DoGoodPickup()
    {
        gameManger.AddScore(scoreValue);

        if (paddle.transform.localScale.x < (maxPaddleSize - paddleSizeValue))
            paddle.transform.localScale += new Vector3(paddleSizeValue, 0, 0);

        else
            paddle.transform.localScale = new Vector3(maxPaddleSize, paddle.transform.localScale.y, paddle.transform.localScale.z);

        goodSound.Play();
    }


    public void SpawnPickup(Vector2 position)
    {

        // This function is called by the ball when it breaks a brick
        // Basically, we cycle through the same pickup, so once this one is disabled (by being collected, or falling off the screen)
        // we reset it's position and activate it again


        if (gameObject.activeSelf) // bounce if we're already in use
            return;

        // set new position and activate
        transform.position = position;
        gameObject.SetActive(true);
    }
}
