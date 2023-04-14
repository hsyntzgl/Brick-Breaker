using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComputerController : MonoBehaviour
{
    private GameObject ball;

    private readonly float leftBorderLimit = 1f;
    private readonly float rightBorderLimit = 9f;
    // Start is called before the first frame update
    void Start()
    {
        ball = GameObject.Find("Ball");
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 newPosition = Vector2.Lerp(transform.position, ball.transform.position, 5 * Time.deltaTime);

        if (newPosition.x > rightBorderLimit)
        {
            newPosition.x = rightBorderLimit;
        }
        else if (newPosition.x < leftBorderLimit)
        {
            newPosition.x = leftBorderLimit;
        }
        transform.position = new Vector2(newPosition.x, transform.position.y);
    }
}
