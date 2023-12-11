using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class JotunSpear : MonoBehaviour
{
    private Rigidbody2D myRigidBody;
    public float velocity;
    public float timeToDestroy;
    private bool isMoving = false;
    private GameObject playerObj;
    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        playerObj = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(!isMoving){
            Vector3 direction = new(0,-1,0);
            myRigidBody.velocity = velocity * direction;
            isMoving = true;
        }

        timeToDestroy -= Time.deltaTime;
        if(timeToDestroy <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}
