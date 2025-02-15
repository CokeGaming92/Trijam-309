using UnityEngine;

public class Paddle : MonoBehaviour
{
    public float speed = 2f;
    public float maxX = 7.0f;
    private float movementHorizontal;

    private void Update()
    {

        //movementHorizontal = Input.GetAxis("Horizontal");

        movementHorizontal = ReadMousePosition().x - transform.position.x;

        if ((movementHorizontal > 0 && transform.position.x < maxX) || (movementHorizontal < 0 && transform.position.x > -maxX))
        {
            transform.position += Vector3.right * movementHorizontal * speed * Time.deltaTime;
        }
    }


    Vector2 ReadMousePosition()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
}
