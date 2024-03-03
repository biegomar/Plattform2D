using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    private Rigidbody2D rb;

    [SerializeField] private Transform feetTrans;
    [SerializeField] private Vector2 checkBoxDimensions;
    [SerializeField] private LayerMask groundLayer;

    /// <summary>
    /// Unity-Awake.
    /// </summary>
    private void Awake()
    {
        this.ShouldJumpAfterTouchDown = false;
        this.NumberOfJumpsUsed = 0;
        this.rb = GetComponent<Rigidbody2D>();
    }    
    
    /// <summary>
    /// Unity-Update.
    /// </summary>
    void Update()
    {
        this.Jump();
        this.MoveHorizontal();
    }

    /// <summary>
    /// Steuert die horizontale Bewegung.
    /// </summary>
    private void MoveHorizontal()
    {
        transform.position = new Vector3(
            CalculateNewXPosition(),
            transform.position.y,
            transform.position.z);
    }

    /// <summary>
    /// Bestimmt die neue X-Position anhand des Input-Systems.
    /// </summary>
    /// <returns></returns>
    private float CalculateNewXPosition()
    {        
        return transform.position.x + Input.GetAxis("Horizontal") * this.Speed * Time.deltaTime;
    }

    /// <summary>
    /// Initiiert den Sprung und die Sprungwiederholung.
    /// </summary>
    private void Jump()
    {
        if (this.rb.velocity.y == 0)
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
        else if (this.rb.velocity.y == 0 && this.ShouldJumpAfterTouchDown)
        {            
            this.JumpNow();
            this.ShouldJumpAfterTouchDown = false;
        }

        var bottomCollider = Physics2D.OverlapBoxAll(feetTrans.position, checkBoxDimensions, 0, groundLayer);

        if (bottomCollider.Any())
        {
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Max(rb.velocity.y, 0f));
        }
        
    }

    /// <summary>
    /// Hier wird der eigentliche Sprung ausgelöst.
    /// </summary>
    private void JumpNow()
    {
        this.NumberOfJumpsUsed++;
        this.rb.AddForce(Vector2.up * Math.Min(NumberOfJumps * Velocity, Velocity), ForceMode2D.Impulse);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(feetTrans.position, checkBoxDimensions);
    }
}
