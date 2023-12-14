using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThreeSpearAttack : MonoBehaviour
{
    public GameObject spear1;
    private GameObject spear2;
    private GameObject spear3;
    private Vector3 playerPos;
    private GameObject playerObj;
    private bool splitted = false;
    public float timeToSplit;
    public float timeToDestroy;
    private Vector3 direction;
    private Rigidbody2D spear1RigidBody;
    private Rigidbody2D spear2RigidBody;
    private Rigidbody2D spear3RigidBody;
    public float velocity;
    private bool minus = false;
    private Vector3 spear2Direction;
    private Vector3 spear3Direction;

    void Start()
    {
        playerObj = GameObject.FindGameObjectWithTag("Player");
        playerPos = playerObj.transform.position;
        float rnum = Random.Range(0f, 2f);
        if(rnum < 1f)
        {
            minus = true;
            transform.position = new(-transform.position.x, transform.position.y, transform.position.z);
        }
        direction = (playerPos - transform.position).normalized;
        spear1RigidBody = spear1.GetComponent<Rigidbody2D>();
        spear1RigidBody.velocity = direction * velocity;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        spear1.transform.rotation = Quaternion.Euler(0, 0,angle-90);
        
      
    }

    // Update is called once per frame
    void Update()
    {


        if(!splitted && timeToSplit <=0)
    	{
            spear2 = Instantiate(spear1);
            spear3 = Instantiate(spear1);
            if(minus)
            {
                spear2Direction = new Vector3(direction.x-0.2f, direction.y+0.2f, direction.z);
                spear3Direction = new Vector3(direction.x+0.2f, direction.y-0.2f, direction.z);
            }
            else
            {
                spear2Direction = new Vector3(direction.x+0.2f, direction.y+0.2f, direction.z);
                spear3Direction = new Vector3(direction.x-0.2f, direction.y-0.2f, direction.z);
            }
            
            
            spear2.transform.position = new(spear1.transform.position.x,spear1.transform.position.y,spear1.transform.position.z);
            spear3.transform.position = new(spear1.transform.position.x,spear1.transform.position.y,spear1.transform.position.z);

            float angle2 = Mathf.Atan2(spear2Direction.y, spear2Direction.x) * Mathf.Rad2Deg;
            float angle3 = Mathf.Atan2(spear3Direction.y, spear3Direction.x) * Mathf.Rad2Deg;

            spear2.transform.rotation = Quaternion.Euler(0, 0,angle2-90);
            spear3.transform.rotation = Quaternion.Euler(0, 0,angle3-90);

            spear2RigidBody = spear2.GetComponent<Rigidbody2D>();
            spear2RigidBody.velocity = spear2Direction * velocity;

            spear3RigidBody = spear3.GetComponent<Rigidbody2D>();
            spear3RigidBody.velocity = spear3Direction * velocity;
            splitted = true;
        } 

        timeToSplit -= Time.deltaTime;
        timeToDestroy -= Time.deltaTime;
        if(timeToDestroy <= 0)
        {
            Destroy(spear1);
            Destroy(spear2);
            Destroy(spear3);
            Destroy(gameObject);
        }
    }
}
