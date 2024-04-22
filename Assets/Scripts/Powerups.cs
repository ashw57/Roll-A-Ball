using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerups : MonoBehaviour
{
   //An enum is a datatype taht we can specify its values and use
   public enum PowerupType {SpeedUp, SpeedDown}

    public PowerupType myPowerup;              //This objects powerup type
    public float powerupDuration = 5.0f;       //The duration of the powerup
    PlayerController playerController;         //A reference to our player controller

     void Start()
    {
        //Find and assign the player conroller object to this local reference
        playerController = FindObjectOfType<PlayerController>();
    }

    public void UsePowerUp()
    {
        //if this powerup is a speedup powerup, increase the players speed by double
        if (myPowerup == PowerupType.SpeedUp)
            playerController.speed = playerController,baseSpeed * 2;

        //if this powerip is the speedDown powerup, decrease the player controller speed times 3
        if (myPowerup == )
    }
}
