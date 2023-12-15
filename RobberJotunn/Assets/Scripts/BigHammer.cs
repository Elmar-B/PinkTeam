using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigHammer : MonoBehaviour
{
    public float velocity;
    private Vector3 playerPos;
    private GameObject playerObj;
    private Rigidbody2D myRigidBody;
    private bool isMoving = false;
    public float rotation;
    public float yHighOutOfBound;
    public float yLowOutOfBound;
    public float xHighOutOfBound;
    public float xLowOutOfBound;
    private bool outOfBounds = false;
    private bool hammerReturning;
    private float returnTime;
    private float timePassed;
    private Vector3 startPosition;
    

    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        playerObj = GameObject.FindGameObjectWithTag("Player");
        playerPos = playerObj.transform.position;
        hammerReturning = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(!hammerReturning)
        {
            CheckOutOfBound();
            if(!isMoving || outOfBounds){
                playerPos = playerObj.transform.position;
                Vector3 direction = (playerPos - transform.position).normalized;
                myRigidBody.velocity = direction * velocity;
                isMoving = true;
            }
        }
        else
        {
            myRigidBody.velocity = Vector3.zero;
            timePassed += Time.deltaTime/returnTime;
            transform.position = Vector3.Lerp(startPosition, playerObj.transform.position, timePassed);
            transform.localScale = Vector3.Lerp(Vector3.one, Vector3.one*0.1f, timePassed);
        }

        Vector3 addRotation = new Vector3 (0,0,rotation);
        transform.Rotate(addRotation);
    }

    private void CheckOutOfBound()
    {
        if(transform.position.x < xLowOutOfBound)
            outOfBounds = true;
        else if(transform.position.x > xHighOutOfBound)
            outOfBounds = true;
        else if(transform.position.y < yLowOutOfBound)
            outOfBounds = true;
        else if(transform.position.y > yHighOutOfBound)
            outOfBounds = true;
        else
            outOfBounds = false;
        
    }

    // Start returning hammer to thor
    public void ReturnHammer(float returnTime)
    {
        Debug.Log("Hammer Returning");
        this.returnTime = returnTime;
        hammerReturning = true;
        startPosition = transform.position;
        gameObject.GetComponent<Collider2D>().tag = "Mjolnir";
        timePassed = 0;
    }
}
