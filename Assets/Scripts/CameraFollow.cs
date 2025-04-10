/*****************************************************************************
// File Name : CameraFollow.cs
// Author : Thomas Santini
// Creation Date : March 20th, 2025 
//
// Brief Description : This script allows the camera to constantly follow
the player's position at a set offset.
*****************************************************************************/
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Vector3 offset;

    /// <summary>
    /// Happens a fraction of a second after Update and FixedUpdate
    /// </summary>
    private void LateUpdate()
    {
        transform.position = player.transform.position + offset;
    }
}
