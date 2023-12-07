using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class OneSpreadBAllController : MonoBehaviour
{
    public float velocity;
    private Rigidbody2D myRigidBody;
    private float timeToDestroy = 100;
    private bool isMoving = false;
    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
    
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        if(!isMoving)
        {
            Vector3 direction = new Vector3(0,-1,0);
            myRigidBody.velocity = direction * velocity;
            isMoving = true;
        }

        Vector3 addRotation = new Vector3 (0,0,5);
        transform.Rotate(addRotation);

        timeToDestroy -=Time.deltaTime;
        if(timeToDestroy <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }

}
