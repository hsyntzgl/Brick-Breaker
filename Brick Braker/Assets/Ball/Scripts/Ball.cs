using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public static Ball instance;

    public bool computerPlaying = false;

    private Rigidbody2D ballRB;

    private readonly float ballForce = 5f;

    private readonly float offsetPaddleX = 0.5f;
    private readonly float offsetPaddleY = 0.2f;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        ballRB = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            if (!computerPlaying)
            {
                SetBallPositionToPaddle();
                ComputerStartPlaying();
                computerPlaying = true;
            }
        }
        else
        {
            if (!LevelManager.instance.IsGameStarted)
            {
                SetBallPositionToPaddle();
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    LevelManager.instance.IsGameStarted = true;
                    ballRB.AddForce((Vector2.right + Vector2.up) * ballForce, ForceMode2D.Impulse);
                }
            }
        }

    }
    private void ComputerStartPlaying()
    {
        ballRB.AddForce((Vector2.right + Vector2.up) * ballForce, ForceMode2D.Impulse);
    }
    private void SetBallPositionToPaddle()
    {
        Vector2 paddlePosition = Paddle.instance.gameObject.transform.position;
        transform.position = new Vector2(paddlePosition.x + offsetPaddleX, paddlePosition.y + offsetPaddleY);
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Brick")) other.gameObject.SetActive(false);
    }
    private void OnBecameInvisible()
    {
        LevelManager.instance.BallOutOfScreen();
        ballRB.velocity = Vector2.zero;
    }

}
