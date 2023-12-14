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
    

    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        playerObj = GameObject.FindGameObjectWithTag("Player");
        playerPos = playerObj.transform.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        checkOutOfBound();
        if(!isMoving ||outOfBounds){
            playerPos = playerObj.transform.position;
            Vector3 direction = (playerPos - transform.position).normalized;
            myRigidBody.velocity = direction * velocity;
            isMoving = true;
        }
        Vector3 addRotation = new Vector3 (0,0,rotation);
        transform.Rotate(addRotation);
        
    }

    private void checkOutOfBound()
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
}
