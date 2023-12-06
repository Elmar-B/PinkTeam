using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class JotunnHands : MonoBehaviour
{

    private Vector3 finalPos = new Vector3 (0.75f, 0.4f, 0.0f);
    public float velocity;
    private Rigidbody2D myRigidBody;
    private BoxCollider2D myBoxCollider;
    private bool moving = true;
    private bool attacking = true;
    public GameObject damageController;
    public GameObject damageControllerChild;
    private BoxCollider2D damageControllerBoxCollider;
    private CapsuleCollider2D damageControllerCapsuleCollider;
    private CapsuleCollider2D damageControllerCapsuleCollider2;
    private float timer = 0;
    private SpriteRenderer rend;
    public CapsuleCollider2D myCapsuleCollider;
    public CapsuleCollider2D myCapsuleCollider2;
    
    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        myRigidBody = GetComponent<Rigidbody2D>();
        myBoxCollider = GetComponent<BoxCollider2D>();
        rend.enabled = false;

        damageControllerBoxCollider = damageController.GetComponent<BoxCollider2D>();
        damageControllerCapsuleCollider2 = damageControllerChild.GetComponent<CapsuleCollider2D>();
        damageControllerCapsuleCollider = damageController.GetComponent<CapsuleCollider2D>();

        myBoxCollider.enabled = !myBoxCollider.enabled;
        myCapsuleCollider.enabled = !myCapsuleCollider.enabled;
        myCapsuleCollider2.enabled = !myCapsuleCollider2.enabled;

        damageControllerBoxCollider.enabled = !damageControllerBoxCollider.enabled;
        damageControllerCapsuleCollider.enabled = !damageControllerCapsuleCollider.enabled;
        damageControllerCapsuleCollider2.enabled = !damageControllerCapsuleCollider2.enabled;
        transform.position = new Vector3 (0.75f, 1.69f, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y<0.80)
        {
            rend.enabled = true;
        }
        
        if(finalPos.y >= transform.position.y)
        {
            if(attacking){
                Debug.Log("Got in");
                myRigidBody.velocity = new Vector3(0f,0f,0f);
                if(!moving)
                {
                    

                    // rend.SetColor("_Color", new Color(11f,98f,147f));
                    //rend.color = new Color(0.04f, 0.38f, 0.58f, 1);
                    rend.color = new Color(1,1,1,1);
                    myBoxCollider.enabled = !myBoxCollider.enabled;
                    myCapsuleCollider.enabled = !myCapsuleCollider.enabled;
                    myCapsuleCollider2.enabled = !myCapsuleCollider2.enabled;

                    damageControllerBoxCollider.enabled = !damageControllerBoxCollider.enabled;
                    damageControllerCapsuleCollider.enabled = !damageControllerCapsuleCollider.enabled;
                    damageControllerCapsuleCollider2.enabled = !damageControllerCapsuleCollider2.enabled;
                    attacking = false;
                }
                if(moving)
                {
                    //damageControllerBoxCollider.enabled = !damageControllerBoxCollider.enabled;
                    if(timer < 1)
                    {
                        if(timer == 0)
                        {
                            damageControllerBoxCollider.enabled = !damageControllerBoxCollider.enabled;
                            damageControllerCapsuleCollider.enabled = !damageControllerCapsuleCollider.enabled;
                            damageControllerCapsuleCollider2.enabled = !damageControllerCapsuleCollider2.enabled;
                            Debug.Log("Damage Time");
                        }
                        timer += Time.deltaTime;
                    }
                    else
                        moving = false;
                    
                    
                }
            }
        }
        else
        {
            Vector3 direction = (finalPos - transform.position).normalized;
            myRigidBody.velocity = direction * velocity * Time.deltaTime;
        }
    }

}
