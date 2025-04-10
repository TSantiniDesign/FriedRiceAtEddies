/*****************************************************************************
// File Name : DoorController.cs
// Author : Thomas Santini
// Creation Date : March 25th, 2025 
//
// Brief Description : This script is for the door buttons. When the player
enters the button trigger, the door is set to active, and when they leave,
they are set to false.
*****************************************************************************/
using UnityEngine;

public class DoorController : MonoBehaviour
{
    [SerializeField] private GameObject door;

    /// <summary>
    /// When the player enters the button trigger, the door is set to active.
    /// </summary>
    /// <param name="other">The object that enters the trigger area</param>
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            door.SetActive(true);
        }
    }

    /// <summary>
    /// When the player exits the button trigger, the door is set to inactive.
    /// </summary>
    /// <param name="other">The object that exits the trigger area</param>
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            door.SetActive(false);
        }
    }
}
