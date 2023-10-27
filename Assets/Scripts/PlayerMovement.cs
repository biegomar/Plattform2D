using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private int Speed;

    [SerializeField]
    private float Velocity;

    [SerializeField]
    private int NumberOfJumps;

    private int NumberOfJumpsUsed;   
    private bool ShouldJumpAfterTouchDown;

    private Rigidbody2D Rigidbody;

    private void Awake()
    {
        this.ShouldJumpAfterTouchDown = false;
        this.NumberOfJumpsUsed = 0;
        this.Rigidbody = GetComponent<Rigidbody2D>();
    }    
    
    void Update()
    {
        this.Jump();
        this.MoveHorizontal();
    }

    private void MoveHorizontal()
    {
        transform.position = new Vector3(
            CalculateNewXPosition(),
            transform.position.y,
            transform.position.z);
    }

    private float CalculateNewXPosition()
    {        
        return transform.position.x + Input.GetAxis("Horizontal") * this.Speed * Time.deltaTime;
    }

    private void Jump()
    {
        if (this.Rigidbody.velocity.y == 0)
        {
            this.NumberOfJumpsUsed = 0;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {                        
            if (this.NumberOfJumpsUsed < NumberOfJumps)
            {
                this.JumpNow();
            }            
            else
            {
                this.ShouldJumpAfterTouchDown = true;         
            }            
        }
        else if (this.Rigidbody.velocity.y == 0 && this.ShouldJumpAfterTouchDown)
        {            
            this.JumpNow();
            this.ShouldJumpAfterTouchDown = false;
        }       
    }

    private void JumpNow()
    {
        this.NumberOfJumpsUsed++;
        this.Rigidbody.AddForce(Vector2.up * Math.Min(NumberOfJumps * Velocity, Velocity), ForceMode2D.Impulse);
    }
}
