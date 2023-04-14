using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    public static Paddle instance;

    private readonly float leftBorderLimit = 1f;
    private readonly float rightBorderLimit = 9f;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    void Update()
    {
        transform.position = PaddlePosition();
    }
    private Vector2 PaddlePosition()
    {
        float cursorPositionX = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;

        if (cursorPositionX > rightBorderLimit)
        {
            cursorPositionX = rightBorderLimit;
        }
        else if (cursorPositionX < leftBorderLimit)
        {
            cursorPositionX = leftBorderLimit;
        }

        return new Vector2(cursorPositionX, transform.position.y);
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            Rigidbody2D BallRB = other.gameObject.GetComponent<Rigidbody2D>();

            float ballPointX = other.GetContact(0).point.x;
            float paddleCenterX = transform.position.x;
            float distanceOfCenter = paddleCenterX - ballPointX;

            BallRB.velocity = Vector2.zero;

            if (paddleCenterX > ballPointX)
            {
                BallRB.AddForce(new Vector2(-(distanceOfCenter * 5f), 5f), ForceMode2D.Impulse);
            }
            else
            {
                BallRB.AddForce(new Vector2(-(distanceOfCenter * 5f), 5f), ForceMode2D.Impulse);
            }
        }
    }
}
