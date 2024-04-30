using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed = 10f;
    [HideInInspector]
    public float baseSpeed;
    private Rigidbody rb;
    public int pickUpCount;
    private Timer timer;
    private bool gameOver = false;
    GameObject resetPoint;
    bool resetting = false;
    bool grounded = true;
    Color originalColour;

    //Controllers
    CameraController cameraController;
    SoundController soundController;


    [Header("UI")]
    public GameObject gameOverScreen;
    public TMP_Text pickUpText;
    public TMP_Text timerText;
    public TMP_Text winTimeText;
   

    // Start is called before the first frame update
    void Start()
    {
        baseSpeed = speed;
        rb = GetComponent<Rigidbody>();
        //Get the number of pick ups in our scene
        pickUpCount = GameObject.FindGameObjectsWithTag("Pick Up").Length;
        //Run the Check Pickups Function
        CheckPickUps();
        gameOverScreen.SetActive(false);
        //Get the timer object and start the timer 
        timer = FindObjectOfType<Timer>();
        timer.StartTimer();
        resetPoint = GameObject.Find("Reset Point");
        originalColour = GetComponent<Renderer>().material.color;
        cameraController = FindObjectOfType<CameraController>();
        soundController = FindObjectOfType<SoundController>();
      
    }

    private void Update()
    {
        timerText.text = "Time: " + timer.GetTime().ToString("F2");

    }
    
     void FixedUpdate()
    {
        if(resetting)
            return;

        if(grounded)
        {
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");
            Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
            rb.AddForce(movement * speed);

            if(cameraController.cameraStyle == CameraStyle.Free)
            {
                //Rotates the player to the direction of the camera
                Vector3 eulerAngles = Camera.main.transform.eulerAngles;
                transform.eulerAngles = eulerAngles;
                //translates the input vectors into coordinates
                movement = transform.TransformDirection(movement);
            }
        }
           
    }

    private void OnTriggerEnter(Collider other)
    {
       if(other.tag == "Pick Up")
        {
            //destroy collided object
            Destroy(other.gameObject);
            //decrement pickup count
            pickUpCount--;
            //run check pickups function
            CheckPickUps();
            soundController.PlayPickupSound();
        } 

       if(other.gameObject.CompareTag("Powerup"))
        {
            other.GetComponent<Powerups>().UsePowerup();
            other.gameObject.transform.position = Vector3.down * 1000;
        }
    }

    
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Respawn"))
        {
            StartCoroutine(ResetPlayer());
        }
    else if(collision.gameObject.CompareTag("Wall"))
        {
            soundController.PlayCollisionSound(collision.gameObject);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))
            grounded = true;
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.CompareTag("Ground"))
            grounded = false;
    }

    public IEnumerator ResetPlayer()
    {
        resetting = true;
        GetComponent<Renderer>().material.color = Color.black;
        rb.velocity = Vector3.zero;
        Vector3 startPos = transform.position;
        float resetSpeed = 2f;
        var i = 0.0f;
        var rate = 1.0f / resetSpeed;
        while (i < 1.0f)
        {
            i += Time.deltaTime * rate;
            transform.position = Vector3.Lerp(startPos, resetPoint.transform.position, i);
            yield return null;
        }
        GetComponent<Renderer>().material.color = originalColour;
        resetting = false;

    }
   
    void CheckPickUps()
    {
        pickUpText.text = "Pick Ups Left:" + pickUpCount;
        if (pickUpCount == 0)
        {
            WinGame(); 
        }
    }

    void WinGame()
    {
        //Set our game over to true
        gameOver = true;
        pickUpText.color = Color.red;
        pickUpText.fontSize = 50;
        //Turn on game over screen
        gameOverScreen.SetActive(true);
        //Stop the timer
        timer.StopTimer();
        //Display our time to the win time text
        winTimeText.text = "You Win! " + timer.GetTime().ToString("F2");

        //Stop the ball from moving
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;

        //Play audio when level completed
        soundController.PlayWinSound();
    }


    public void QuitGame()
    {
        Application.Quit();
    }

   
  

}
