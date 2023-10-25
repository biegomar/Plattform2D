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

    private Rigidbody2D Rigidbody;

    private bool shouldJumpAfterTouchDown;

    // Start is called before the first frame update
    void Start()
    {
        this.shouldJumpAfterTouchDown = false;
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
        return Math.Min(8.28f, Math.Max(-8.25f, transform.position.x + Input.GetAxis("Horizontal") * this.Speed * Time.deltaTime));        
    }

    private void Jump()
    {

        //if (Input.GetAxis("Vertical") * Time.deltaTime  > 0)        
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            if (this.NumberOfJumpsUsed < NumberOfJumps)
            {
                this.JumpNow();
            }
            else if (this.Rigidbody.velocity.y == 0)
            {
                this.NumberOfJumpsUsed = 0;
            }
            else
            {
                this.shouldJumpAfterTouchDown = true;         
            }
        }
        else if (this.Rigidbody.velocity.y == 0 && this.shouldJumpAfterTouchDown)
        {            
            this.JumpNow();
            this.shouldJumpAfterTouchDown = false;
        }       
    }

    private void JumpNow()
    {
        this.NumberOfJumpsUsed++;
        this.Rigidbody.AddForce(Vector2.up * Math.Min(NumberOfJumps * Velocity, Velocity), ForceMode2D.Impulse);
    }
}
