using System.Collections;
using System.Collections.Generic;
using System.Runtime;
using UnityEngine;


public class BulletConroller : MonoBehaviour
{
    private Rigidbody2D myRigidBody;
    public float timeToDestroy;
    public float velocity;
    private Vector3 playerPos;
    private GameObject playerObj;
    private bool isMoving = false;
    // Start is called before the first frame update
    void Start()
    {
        myRigidBody = GetComponent<Rigidbody2D>();
        playerObj = GameObject.FindGameObjectWithTag("Player");
        playerPos = playerObj.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if(!isMoving){
            Vector3 direction = (playerPos - transform.position).normalized;
            myRigidBody.velocity = direction * velocity;

            isMoving = true;
        }


        timeToDestroy -= Time.deltaTime;
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
