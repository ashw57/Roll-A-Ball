using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerups : MonoBehaviour
{
    //An enum is a datatype that we can specify its values and use
    public enum PowerupType {SpeedUp, SpeedDown, Grow, Shrink, PauseTimer, SlowMo}
    
    public PowerupType myPowerup;              //This objects powerup type
    public float powerupDuration = 4f;         //The duration of the powerup
    PlayerController playerController;         //A reference to our player controller
    Timer timer;                               //A reference to our timer

     void Start()
    {
        //Find and assign the player conroller object to this local reference
        playerController = FindObjectOfType<PlayerController>();

        //This line will change myPowerup type to be random each time
        myPowerup = (PowerupType)Random.Range(0, System.Enum.GetValues(typeof(PowerupType)).Length);

        //Find and assign the timer object to this local reference
        timer = FindObjectOfType<Timer>();
    }

    public void UsePowerup()
    {
        //if this powerup is the grow powerup, increase the player controller size times 2
        //We also need to move on the u axis otherwise it will go through the gorund collider 
        if (myPowerup == PowerupType.Grow)
        {
            Vector3 temp = playerController.gameObject.transform.position;
            playerController.gameObject.transform.position = temp;
            playerController.gameObject.transform.localScale = Vector3.one * 2;

        }
        //If this powerup is the shrink powerup, decrease the player size by half
        if (myPowerup == PowerupType.Shrink)
           playerController.gameObject.transform.localScale = Vector3.one / 2;

        //if this powerup is a speedup powerup, increase the players speed by triple
        if (myPowerup == PowerupType.SpeedUp)
            playerController.speed = playerController.baseSpeed * 3;
        //if this powerip is the speedDown powerup, decrease the player controller speed times 3
        if (myPowerup == PowerupType.SpeedDown)
            playerController.speed = playerController.baseSpeed / 3;

        //If this powerup is the pause powerup, pause the timer in the time script
        if (myPowerup == PowerupType.PauseTimer)
            timer.PauseTimer(true);
        //If this powerup is the SlowMo powerup, pause the timer in the timer script
        if (myPowerup == PowerupType.SlowMo)
            timer.ChangeTimeScale(0.4f);

        //Start a couroutine to reset the powerups effects
        StartCoroutine(ResetPowerup());
    }

    IEnumerator ResetPowerup()
    { 
        yield return new WaitForSeconds(powerupDuration);

        //If this powerup relates to size, reset our player controller size to 1
        if (myPowerup == PowerupType.Grow || myPowerup == PowerupType.Shrink)
        {
            playerController.gameObject.transform.localScale = Vector3.one;
        }
        //If this powerup relates to speed, reset our player controller speed to its base speed
        if (myPowerup == PowerupType.SpeedUp || myPowerup == PowerupType.SpeedDown)
            playerController.speed = playerController.baseSpeed;

        //If this powerup is the pause, unpase the timer in the timer script
        if (myPowerup == PowerupType.PauseTimer)
            timer.PauseTimer(false);
        //If this powerup is the slowmo powerup, set the timescale back to 1\
        if (myPowerup == PowerupType.SlowMo)
            timer.ChangeTimeScale(1);
    }
}
