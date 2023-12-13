using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpreadBallAttackController : MonoBehaviour
{
    public GameObject ball;
    public float velocity;
    private float nextBallXPos = 0.2f;
    private int count = 0;
    private float timer;
    

    // Update is called once per frame
    void Update()
    {
        if(count < 8)
        {
            timer = 0;
            if(count == 0)
            {
                GameObject ballAttack = Instantiate(ball);
                ballAttack.transform.position = new Vector3(0,1,0);
                OneSpreadBAllController script = ballAttack.GetComponent<OneSpreadBAllController>();
                script.velocity = velocity;
                count++;
            }
            else
            {
                GameObject ballAttack = Instantiate(ball);
                ballAttack.transform.position = new Vector3 (count*nextBallXPos, 1, 0);
                OneSpreadBAllController script = ballAttack.GetComponent<OneSpreadBAllController>();
                script.velocity = velocity;

                GameObject nextBallAttack = Instantiate(ball);
                nextBallAttack.transform.position = new Vector3 (-count*nextBallXPos, 1, 0);
                OneSpreadBAllController script2 = ballAttack.GetComponent<OneSpreadBAllController>();
                script2.velocity = velocity;
                count++;
            }

            while(timer <= 2)
            {
                timer += Time.deltaTime;
            }
        
            
        } 
    }

    // void Start()
    // {
    //     for(int i = 0; i<8; i++)
    //     {
    //         if(i == 0)
    //         {
    //             GameObject ballAttack = Instantiate(ball);
    //             ballAttack.transform.position = new Vector3(0,1,0);
    //             OneSpreadBAllController script = ballAttack.GetComponent<OneSpreadBAllController>();
    //             script.velocity = velocity;
    //         }
    //         else
    //         {
    //             GameObject ballAttack = Instantiate(ball);
    //             ballAttack.transform.position = new Vector3 (i*nextBallXPos, 1, 0);
    //             OneSpreadBAllController script = ballAttack.GetComponent<OneSpreadBAllController>();
    //             script.velocity = velocity;

    //             GameObject nextBallAttack = Instantiate(ball);
    //             nextBallAttack.transform.position = new Vector3 (-i*nextBallXPos, 1, 0);
    //             OneSpreadBAllController script2 = ballAttack.GetComponent<OneSpreadBAllController>();
    //             script2.velocity = velocity;
    //         }
            
    //     } 
    // }
}
