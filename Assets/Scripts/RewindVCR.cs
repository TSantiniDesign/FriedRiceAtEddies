/*****************************************************************************
// File Name : RewindVCR.cs
// Author : Thomas Santini
// Creation Date : March 23rd, 2025 
//
// Brief Description : This script is for the VCR mechanic. It creates a
timer that slowly ticks down, but will tick back up while the player is in
contact with the trigger of the VCR button. If the timer reaches 0, the
player dies.
*****************************************************************************/
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RewindVCR : MonoBehaviour
{
    [SerializeField] private TMP_Text timerText;
    [SerializeField] private TMP_Text deathText;
    [SerializeField] private int countdownTimer = 30;
    [SerializeField] private AudioClip tickSound;
    [SerializeField] private AudioSource deathSound;
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject television;
    [SerializeField] private Vector3 offset;
    private bool hasDied = false;
    [SerializeField] private bool isTutorial;
    public int setFree;
    [SerializeField] private GameObject cage;
    private bool hasPressed = false;

    /// <summary>
    /// When the game starts, the timer begins counting down at a standard rate, unless it is the tutorial.
    /// </summary>
    private void Start()
    {
        if (isTutorial == false)
        {
            InvokeRepeating("TimerDecrease", 3, 1);
        }
    }

    /// <summary>
    /// Cancels the current function being invoked.
    /// </summary>
    public void StopInvoke()
    {
        CancelInvoke();
    }

    /// <summary>
    /// Invokes the timer decrease repeatedly, unless it is the tutorial.
    /// </summary>
    public void StartInvoke()
    {
        if (isTutorial == false)
        {
            InvokeRepeating("TimerDecrease", 0, 1);
        }
    }

    /// <summary>
    /// While the player is inside the trigger on the VCR rewind button, the time left will increase at a fast rate.
    /// </summary>
    /// <param name="other">The object that enters the trigger area</param>
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            InvokeRepeating("RewindFunction", 0, .15f);
            if (isTutorial == true && hasPressed == false)
            {
                setFree++;
                hasPressed = true;
            }
        }
    }

    /// <summary>
    /// When called, this function decreases the timer int amount, and updates the timertext to reflect that.
    /// </summary>
    void TimerDecrease()
    {
        AudioSource.PlayClipAtPoint(tickSound, transform.position, 1);
        countdownTimer--;
        timerText.text = countdownTimer.ToString();
    }

    /// <summary>
    /// When called, this function increases the timer int amount, and updates the timertext to reflect that.
    /// </summary>
    void RewindFunction()
    {
        if (countdownTimer <= 59)
        {
            countdownTimer++;
        }
        timerText.text = countdownTimer.ToString();
    }

    /// <summary>
    /// When the player exits the trigger area on the VCR button, the rewindfunction stops being invoked repeatedly.
    /// </summary>
    /// <param name="other">The object that exits the trigger area</param>
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            CancelInvoke("RewindFunction");
        }
    }

    /// <summary>
    /// Stops the timer from decreasing further, then invokes the ReloadScene function after a slight delay.
    /// </summary>
    private void Die()
    {
        deathSound.Play();
        television.transform.position = player.transform.position + offset;
        CancelInvoke("TimerDecrease");
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
    /// If the countdown timer reaches zero, the player dies.
    /// </summary>
    private void Update()
    {
        if (countdownTimer <= 0)
        {
            if (!hasDied)
            {
                Die();
                hasDied = true;
            }
        }
        if (setFree >= 3)
        {
            cage.SetActive(false);
        }
    }
}
