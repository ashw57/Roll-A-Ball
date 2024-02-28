using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public float speed = 1f;
    private Rigidbody rb;
    public int pickUpCount;
    private Timer timer;

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

   
    void FixedUpdate()
    {
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
        print("Pick Ups Left:" + pickUpCount);
        if (pickUpCount == 0)
        {
            timer.StopTimer();
            print("Yay! You Won. Your Time Was: " + timer.GetTime());
        }
    }
}
