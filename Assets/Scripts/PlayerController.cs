using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using TMPro;

public class PlayerController : MonoBehaviour
{
    public float speed = 1f;
    private Rigidbody rb;
    public int pickUpCount;
    private Timer timer;
    private bool gameOver = false;


    [Header("UI")]
    public GameObject gameOverScreen;
    public TMP_Text pickUpText;
    public TMP_Text timerText;
    public TMP_Text winTimeText;
   

    // Start is called before the first frame update
    void Start()
    {
       
        rb = GetComponent<Rigidbody>();
        //Get the number of pick ups in our scene
        pickUpCount = GameObject.FindGameObjectsWithTag("Pick Up").Length;
        //Run the Check Pickups Function
        CheckPickUps();
        //Get the timer object and start the timer 
        timer = FindObjectOfType<Timer>();
        timer.StartTimer();
      
    }

    private void Update()
    {
        timerText.text = "Time: " + timer.GetTime().ToString("F2");

    }
    

   
    void FixedUpdate()
    {
        if (gameOver == true)
            return;

        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0, moveVertical);
        rb.AddForce(movement * speed);
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
        }

        
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
    }

    //Temporary - Remove when doing A2 modules
    public void RestartGame()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

   
  

}
