using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FlyingSword : MonoBehaviour
{
    private Rigidbody2D myRigidBody;
    public float timeToDestroy;
    public float velocity;
    private Vector3 playerPos;
    private GameObject playerObj;
    private bool isMoving = true;
    private bool isAttacking = true;
    private Vector3 originalPos;
    public float spinningVelocity;
    // Start is called before the first frame update
    void Start()
    {
        float rnum =Random.Range(0f, 2f);
        if(rnum < 1f)
        {
            transform.position = new(1,1,0);
        }
        else if(rnum < 2f)
        {
            transform.position = new(-1,1,0);
        }
        myRigidBody = GetComponent<Rigidbody2D>();
        playerObj = GameObject.FindGameObjectWithTag("Player");
        originalPos = transform.position;
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(isMoving)
        {
            if(isAttacking)
            {
                playerPos = playerObj.transform.position;
                Vector3 direction = (playerPos - transform.position).normalized;
                myRigidBody.velocity = direction * velocity;
                
            }
            else
            {
                if(transform.position.x == originalPos.x && transform.position.y == originalPos.y)
                {
                    Destroy(gameObject);
                }
                else
                {
                    Vector3 direction = (originalPos - transform.position).normalized;
                    myRigidBody.velocity = direction * velocity;
                }
            }

            Vector3 rotaionToAdd = new Vector3(0, 0, spinningVelocity);
            transform.Rotate(rotaionToAdd);
        }

        else if(transform.position.x == originalPos.x && transform.position.y == originalPos.y)
        {
            isMoving = false;
        }


        timeToDestroy -= Time.deltaTime;
        if(timeToDestroy <= 0)
        {
            Destroy(gameObject);
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Weapon")
        {
            Destroy(gameObject);
        }
        else if(other.gameObject.tag == "Player")
        {
            isAttacking = false;
        }

        else if(other.gameObject.tag == "Damageable")
        {
            if(!isAttacking)
            {
                Destroy(gameObject);
            }
        }
    }
}
