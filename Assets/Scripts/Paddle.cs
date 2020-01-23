using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    [SerializeField] float horizonalUnits = 16f;
    [SerializeField] private float maxX = 15f;
    [SerializeField] private float minX = 1f;
    [SerializeField] Ball ballPrefab;
    [SerializeField] Transform ballBarrelPosition;

    void Update()
    {
        var mousePositionX = Mathf.Clamp(Input.mousePosition.x / Screen.width * horizonalUnits, minX, maxX);
        Vector2 paddlePosition = new Vector2(mousePositionX, transform.position.y);
        transform.position = paddlePosition;

        LaunchBallOnMouseClick();
    }

    private void LaunchBallOnMouseClick()
    {
        if (Input.GetMouseButton(0))
        {
            var ball = Instantiate<Ball>(ballPrefab, ballBarrelPosition.position, transform.rotation);
            ball.paddle = this;
        }
    }
}
