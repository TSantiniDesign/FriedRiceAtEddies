/*****************************************************************************
// File Name : HidingEnemy.cs
// Author : Thomas Santini
// Creation Date : March 30th, 2025 
//
// Brief Description : This script is for the hiding enemy. A countdown 
slowly ticks down, and if it reaches zero and the player is not underneath
the main table, they die.
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HidingEnemy : MonoBehaviour
{
    [SerializeField] private int countdownTimer;
    [SerializeField] private TMP_Text deathText;
    [SerializeField] private TMP_Text hideText;
    private bool isSafe = false;
    [SerializeField] private List<DoorEnemies> doorEnemies = new List<DoorEnemies>();
    private bool isPaused = false;
    [SerializeField] private AudioClip screamSound;
    private bool hasPlayed = false;

    /// <summary>
    /// When the game starts, the timer begins decreasing by one every second.
    /// </summary>
    void Start()
    {
        InvokeRepeating("TimerDecrease", 3, 1);
    }

    /// <summary>
    /// The countdown timer decreases by one.
    /// </summary>
    void TimerDecrease()
    {
        countdownTimer--;
    }

    /// <summary>
    /// When the player is inside the trigger underneath the table, isSafe becomes true.
    /// </summary>
    /// <param name="other">The object that enters the trigger area</param>
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isSafe = true;
        }
    }

    /// <summary>
    /// While the player exits the trigger underneath the table, isSafe becomes false.
    /// </summary>
    /// <param name="other">The object that enters the trigger area</param>
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            isSafe = false;
        }
    }

    /// <summary>
    /// Stops the timer from decreasing further, activates the death text and changes it to show what enemy they died
    /// to, then invokes the ReloadScene function after a slight delay.
    /// </summary>
    private void Die()
    {
        CancelInvoke("TimerDecrease");
        deathText.text = "You died to the Hiding enemy";
        deathText.gameObject.SetActive(true);
        Invoke("ReloadScene", 1.5f);
    }

    /// <summary>
    /// Uses the scene manager to send the player to the game over screen.
    /// </summary>
    void ReloadScene()
    {
        SceneManager.LoadSceneAsync("GameOver");
    }

    /// <summary>
    /// When this coroutine is called, the two door enemies will stop in place for a set amount of time, then return
    /// to moving like normal.
    /// </summary>
    private IEnumerator PauseDoors()
    {
        if (!hasPlayed)
        {
            hasPlayed = true;
            AudioSource.PlayClipAtPoint(screamSound, transform.position, 2);
        }
        isPaused = true;

        yield return new WaitForSeconds(5);

        hasPlayed = false;
        isPaused = false;
        foreach (DoorEnemies x in doorEnemies)
        {
           x.Speed = 5;
        }
    }

    /// <summary>
    /// While the countdown timer is less than 5, the warning text is active, and the PauseDoors coroutine is started.
    /// If the countdown reaches 0 and isSafe is not true, the player dies. If isSafe is true, the countdown 
    /// timer resets.
    /// </summary>
    void Update()
    {
        if (countdownTimer < 5)
        {
            StartCoroutine(PauseDoors());
            hideText.gameObject.SetActive(true);
        }
        else
        {
            hideText.gameObject.SetActive(false);
        }

        if (isPaused == true)
        {
            foreach (DoorEnemies x in doorEnemies)
            {
                x.Speed = 0;
            }
        }

        if (countdownTimer == 0)
        {

            if(isSafe == false)
            {
                Die();
            }
            else
            {
                countdownTimer = 60;
            }
        }
    }
}
