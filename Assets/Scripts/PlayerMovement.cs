/*****************************************************************************
// File Name : PlayerMovement.cs
// Author : Thomas Santini
// Creation Date : March 20th, 2025 
//
// Brief Description : This script controls the player's movement, using the
input action map to allow the player to move in all four directionsand jump 
based on the keys they press.
*****************************************************************************/
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerInput playerinput;
    [SerializeField] private float jumpValue = 5f;
    [SerializeField] private float playerSpeed = 15f;

    private Rigidbody rb;
    private Vector3 playerMovement;

    private bool jumpedOnce = false;
    private bool jumpedTwice = false;

    /// <summary>
    /// When the game starts, this script grabs the rigidbody attached to the player, and enables the current action
    /// map to be used with movement.
    /// </summary>
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        playerinput.currentActionMap.Enable();
    }

    /// <summary>
    /// When the player presses the spacebar, the will jump up, and are able to jump one more time before needing to
    /// touch the ground to be able to jump again.
    /// </summary>
    void OnJump()
    {
        //rb.velocity = new Vector3(0, jumpValue, 0);
        if (jumpedOnce == false && jumpedTwice == false)
        {
            rb.velocity = new Vector3(0, jumpValue, 0);
            jumpedOnce = true;
        }
        else if (jumpedOnce == true && jumpedTwice == false)
        {
            rb.velocity = new Vector3(0, jumpValue, 0);
            jumpedTwice = true;
        }
    }

    /// <summary>
    /// When the player collides with the floor, they regain the ability to jump.
    /// </summary>
    /// <param name="collision">The object that the player collides with</param>
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Floor"))
        {
            jumpedOnce = false;
            jumpedTwice = false;
        }
    }

    /// <summary>
    /// Gets the value from the keyboard and updates the movement variable on the x and z axis, while multiplying by 
    /// playerSpeed to get the ideal movement speed and direction.
    /// </summary>
    /// <param name="iValue">value read in from the keyboard</param>
    void OnMove(InputValue iValue)
    {
        Vector2 inputMovement = iValue.Get<Vector2>();
        playerMovement.x = inputMovement.x * playerSpeed;
        playerMovement.z = inputMovement.y * playerSpeed;
    }

    void OnReturn(InputValue iValue)
    {
        SceneManager.LoadSceneAsync("MainMenu");
    }

    /// <summary>
    /// Moves the player based off their inputs.
    /// </summary>
    void Update()
    {
        rb.velocity = new Vector3(playerMovement.x, rb.velocity.y, playerMovement.z);
    }

}
