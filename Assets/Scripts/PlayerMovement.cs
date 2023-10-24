using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private int Speed;

    [SerializeField]
    private int JumpAcceleration;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var oldPosition = transform.position;
        var movementX = Input.GetAxisRaw("Horizontal");
        var jump = Input.GetAxisRaw("Vertical");


        oldPosition.x = Math.Min(8.28f, Math.Max(-8.25f, oldPosition.x + CalculateNewXPosition(movementX)));
        //oldPosition.x += CalculateNewXPosition(movementX);
        oldPosition.y += CalculateJump(jump);

        transform.position = oldPosition;
    }

    private float CalculateNewXPosition(float movementX)
    {
        return movementX * this.Speed * Time.deltaTime;
    }

    private float CalculateJump(float jump)
    {
        return Math.Max(0, jump * this.JumpAcceleration * Time.deltaTime);
        //return jump * this.JumpAcceleration * Time.deltaTime;
    }
}
