using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public float speed = 1f;
    private Rigidbody rb;
    public int pickUpCount;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //Get the number of pick ups in our scene
        pickUpCount = GameObject.FindGameObjectsWithTag("PickUp").Length;
        //Run the Check Pickups Function
        CheckPickUps();
      
    }

    // Update is called once per frame
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
            //Destroy the collided object
            Destroy(other.gameObject);
            //Decrement the pick up count
            pickUpCount--;
            //Run the Check Pick Ups function
            CheckPickUps();
        }

        
    }
    void CheckPickUps()
    {
        print("Pick Ups Left:" + pickUpCount);
        if (pickUpCount == 0)
        {
            print("Yay! You Won");
        }
    }
}
