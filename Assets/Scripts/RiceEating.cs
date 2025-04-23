/*****************************************************************************
// File Name : RiceEating.cs
// Author : Thomas Santini
// Creation Date : March 23rd, 2025 
//
// Brief Description : This script is for the Fried Rice mechanic. It creates
a tracker of how much rice has been eaten, and has the value slowly go up
while the player is in contact with a trigger around the fried rice, and stop
increasing when the player leaves.
*****************************************************************************/
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RiceEating : MonoBehaviour
{
    [SerializeField] private TMP_Text riceText;
    [SerializeField] private TMP_Text winText;
    private int riceEaten = 0;
    [SerializeField] private int riceNeeded;

    /// <summary>
    /// If the player exits the trigger area, the repeated invoke of EatFunction is started, which currently
    /// repeats once every second.
    /// </summary>
    /// <param name="other">The object that enters the trigger area</param>
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //Debug.Log("A collider has entered the Rice trigger");
            InvokeRepeating("EatFunction", .5f, 1);
        }
    }

    /// <summary>
    /// When the function is called, the amount of rice that has been eaten is increased by 1, and the riceText is
    /// updated to reflect that.
    /// </summary>
    void EatFunction()
    {
        riceEaten++;
        riceText.text = "Rice Eaten: " + riceEaten.ToString() + "/" + riceNeeded.ToString();
    }

    /// <summary>
    /// If the player exits the trigger area, the repeated invoke of EatFunction is canceled.
    /// </summary>
    /// <param name="other">The object that exits the trigger area</param>
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //Debug.Log("A collider has exited the Rice trigger");
            CancelInvoke();
        }
    }

    /// <summary>
    /// Activates the win text and changes it to that they won, then invokes the ReloadScene function after
    /// a slight delay.
    /// </summary>
    private void Win()
    {
        riceText.gameObject.SetActive(false);
        winText.text = "You ate all the rice! You Won!";
        winText.gameObject.SetActive(true);
        Invoke("ReloadScene", 1.5f);
    }

    /// <summary>
    /// Uses the scene manager to send the player back to the main menu.
    /// </summary>
    void ReloadScene()
    {
        SceneManager.LoadSceneAsync("WinScene");
    }

    /// <summary>
    /// If the amount of rice eaten equals the amount of rice needed, the player wins
    /// </summary>
    private void Update()
    {
        if (riceEaten >= riceNeeded)
        {
            Win();
        }
    }
}
