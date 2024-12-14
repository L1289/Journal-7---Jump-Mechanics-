using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
    public enum FacingDirection
    {
        left, right
    }

    public float speed;
    public Rigidbody2D playerBody;
    private Vector2 velocity;
    public float acceleration = 3f;
    public float postiveSpeedLimit = 6f;
    public float negativeSpeedLimit = 6f;
    private FacingDirection direction = FacingDirection.right;


    public float apHeight = 7f;
    public float apTime = 0.7f;
    private float gravity;
    public float jumpForce;

    // Start is called before the first frame update
    void Start()
    {
        gravity = -2 * apHeight / (apTime * apTime);
        jumpForce = 2 * apHeight / apTime;
    }

    // Update is called once per frame
    void Update()
    {
        IsGrounded();
        Debug.Log(IsGrounded());
        IsWalking();
        // The input from the player needs to be determined and
        // then passed in the to the MovementUpdate which should
        // manage the actual movement of the character.

        Vector2 playerInput = new Vector2();
        //Gets inputs from keybinds A and D 
        playerInput.x = Input.GetAxisRaw("Horizontal");

        //Gets Inputs from Keybinds W and S
        playerInput.y = Input.GetAxisRaw("Vertical");
        MovementUpdate(playerInput);

        playerBody.velocity = velocity;
    }

    private void MovementUpdate(Vector2 playerInput)
    {
        if (playerInput.x != 0)
        {
            //moves the player and updates their postion based on the current speed they are going at 
            velocity.x += acceleration * playerInput.x * Time.deltaTime;
            velocity.x = Mathf.Clamp(velocity.x, -negativeSpeedLimit, postiveSpeedLimit);
        }
        
        else
        {
            velocity.x = 0;
        }

        //changes the players direction they are facing based on the velocity they are traveling at and how much.
        if (velocity.x > 0)
        {
            direction = FacingDirection.right;
        }
        else if (velocity.x < 0)
        {
            direction = FacingDirection.left;
        }

        if (playerInput.y > 0 && IsGrounded = true)
        {
            velocity.y = jumpForce;
        }

    }

    public bool IsWalking()
    {
        //Checks if their is velocity happing on the x axis if so then it will return true that the player is walking switching the animation being played from idle to walking
        if (velocity.x != 0)
        {
            return true;
        }
        //otherwise it will just return false and keep playing idle
        else
        {
            return false;
        }
    }
    public bool IsGrounded()
    {
        //if the player falls or jumps and the velocity on the y axis changes and is not 0 then the player will not be grounded as it will be returns false
        if (velocity.y != 0)
        {
            return false;
        }
        //Otherwise the player will be grounded as it will be returned true as long as the y is zero.
        else
        {
            return true;
        }
    }

    public FacingDirection GetFacingDirection()
    {
        return direction;
    }
}
