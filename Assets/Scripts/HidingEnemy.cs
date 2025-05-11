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
    [SerializeField] private AudioSource warnSound;
    [SerializeField] private AudioSource deathSound;
    private bool hasPlayed = false;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject creature;
    [SerializeField] private Vector3 offset;
    private bool hasDied = false;
    [SerializeField] private int minTimer;
    [SerializeField] private int maxTimer;
    private bool corReady = true;

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
    /// Stops the timer from decreasing further, plays the jumpscare sound, moves the enemy to the player's position,
    /// then invokes the ReloadScene function after a slight delay.
    /// </summary>
    private void Die()
    {
        deathSound.Play();
        creature.transform.position = player.transform.position + offset;
        CancelInvoke("TimerDecrease");
        //deathText.text = "You died to the Hiding enemy";
        //deathText.gameObject.SetActive(true);
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
            warnSound.Play();
        }
        if (!isPaused)
        {
            foreach (DoorEnemies x in doorEnemies)
            {
                x.SavedSpeed = x.Speed;
                x.Speed = 0;
            }
            isPaused = true;
        }
        //isPaused = true;

        yield return new WaitForSeconds(10);

        corReady = true;
        hasPlayed = false;
        isPaused = false;
        foreach (DoorEnemies x in doorEnemies)
        {
           x.Speed = x.SavedSpeed;
        }
    }

    /// <summary>
    /// While the countdown timer is less than 5, the screeching noise is played, and the PauseDoors coroutine
    /// is started.
    /// If the countdown reaches 0 and isSafe is not true, the player dies. If isSafe is true, the countdown 
    /// timer resets.
    /// </summary>
    void Update()
    {
        if (countdownTimer < 5 && corReady == true)
        {
            corReady = false;
            StartCoroutine(PauseDoors());
            hideText.gameObject.SetActive(true);
        }
        else
        {
            hideText.gameObject.SetActive(false);
        }


        if (countdownTimer == 0)
        {

            if(isSafe == false)
            {
                if(!hasDied)
                {
                    Die();
                    hasDied = true;
                }
                
            }
            else
            {
                countdownTimer = Random.Range(minTimer, maxTimer);
            }
        }
    }
}
