/*****************************************************************************
// File Name : PlayerGravity.cs
// Author : Thomas Santini
// Creation Date : April 8th, 2025 
//
// Brief Description : This script gives the player a stronger gravity when
the game begins.
*****************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGravity : MonoBehaviour
{
    [SerializeField] private int gravity;
    
    /// <summary>
    /// When the game begins, the player's vertical gravity is set to a certain amount.
    /// </summary>
    void Start()
    {
        Physics.gravity = new Vector3(0, gravity, 0);
    }
}
