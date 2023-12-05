using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class JotunnHands : MonoBehaviour
{

    private Vector3 finalPos = new Vector3 (0.62f, 0.31f, 0.0f);
    public float velocity;
    private Rigidbody2D myRigidBody;
    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        transform.position = new Vector3 (0.62f, 1.73f, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Y: "+transform.position.y+"\n");
        if(finalPos.y == transform.position.y)
        {
            Debug.Log("It is the same yeiiiiiiii!");
            myRigidBody.velocity = new Vector3(0f,0f,0f);
        }
        else{
            Vector3 direction = (finalPos - transform.position).normalized;
            myRigidBody.velocity = direction * velocity;
        }
    }
}
