using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{

    [SerializeField] public Paddle paddle;
    [SerializeField] float xPush = 2.0f;
    [SerializeField] float yPush = 15.0f;
    [SerializeField] AudioClip[] ballSounds;

    private AudioSource audioSource;
    private Vector2 paddleToBallVector;
    public bool hasStarted;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        paddleToBallVector = transform.position - paddle.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasStarted)
        {
            LockBallToPaddle();
        }
        LaunchOnMouseClick();
    }

    private void LaunchOnMouseClick()
    {
        if (Input.GetMouseButton(0))
        {
            Vector2 velocityTweak = new Vector2(
                UnityEngine.Random.Range(-2.0f, xPush),
                UnityEngine.Random.Range(0f, yPush)
                );
            GetComponent<Rigidbody2D>().velocity = velocityTweak;
            hasStarted = true;
        }
    }

    private void LockBallToPaddle()
    {
        Vector2 paddlePos = new Vector2(paddle.transform.position.x, paddle.transform.position.y);
        transform.position = paddlePos + paddleToBallVector;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log(collision.gameObject.tag);
        if (hasStarted)
        {
            var clip = ballSounds[UnityEngine.Random.Range(0, ballSounds.Length)];
            audioSource.PlayOneShot(clip);
            if (collision.gameObject.tag != "Ball")
            {
                Destroy(gameObject);
            }
        }
    }
}
