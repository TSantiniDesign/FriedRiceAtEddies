/*****************************************************************************
// File Name : DoorEnemies.cs
// Author : Thomas Santini
// Creation Date : March 29th, 2025 
//
// Brief Description : This script is for the door enemies. They use move
points to get close to the doors, and when they reach the move point next to 
the door, they check to see if the door is closed, rushing back to the start 
if the door is closed, and killing the player if they are not.
*****************************************************************************/
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorEnemies : MonoBehaviour
{
    [SerializeField] private GameObject[] movePoints;
    [SerializeField] private float speed;
    private int currentIndex;
    [SerializeField] private GameObject door;
    [SerializeField] private TMP_Text deathText;

    public float Speed { get => speed; set => speed = value; }

    /// <summary>
    /// When the game starts, the current index is set to 0, ensuring that the first point the object will be at is
    /// the one in slot 0.
    /// </summary>
    void Start()
    {
        currentIndex = 0;
    }

    /// <summary>
    /// Invokes the ReloadScene function after a slight delay.
    /// </summary>
    private void Die()
    {
        Invoke("ReloadScene", 1.5f);
    }

    /// <summary>
    /// Uses the scene manager to send the player back to the main menu.
    /// </summary>
    void ReloadScene()
    {
        SceneManager.LoadSceneAsync("MainMenu");
    }

    /// <summary>
    /// This makes it so that the object continuously move towards the next point in the index, and once it gets
    /// within a certain distance, it adds one to the index, and moves to the next point. When it reaches the maximum
    /// length of the index, it resets, and restarts the order.
    /// 
    /// When the index reaches 0, the position outside the door, is the door is not active, the player dies, but if
    /// the door is closed, the enemy quickly rushes back to its starting position.
    /// </summary>
    void Update()
    {
        if (Vector3.Distance(transform.position, movePoints[currentIndex].transform.position) < 0.1f)
        {
            currentIndex++;

            //if the current undex gets to the end of the array
            if (currentIndex == movePoints.Length)
            {
                //reset the index value
                currentIndex = 0;
            }

            if (currentIndex == 0)
            {
                if (door.activeSelf == false)
                {
                    deathText.text = "You died to the Door enemy";
                    deathText.gameObject.SetActive(true);
                    Die();
                }
                else
                {
                    speed = 30;
                    transform.position = Vector3.MoveTowards(transform.position,
                        movePoints[currentIndex].transform.position, speed * Time.deltaTime);
                }
            }
            else
            {
                speed = 4.5f; //Random.Range(4, 7);
                transform.position = Vector3.MoveTowards(transform.position,
                    movePoints[currentIndex].transform.position, speed * Time.deltaTime);
            }
        }

        
        transform.position = Vector3.MoveTowards(transform.position, movePoints[currentIndex].transform.position,
            speed * Time.deltaTime);
    }
}
