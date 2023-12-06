using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpreadBallAttackController : MonoBehaviour
{
    public GameObject ball;
    private List<GameObject> ballList;
    
    // Start is called before the first frame update
    void Start()
    {
        ballList = new List<GameObject>();
        for(int i = 0; i<10; i++)
        {
            GameObject ballAttack = Instantiate(ball);
            // GameObject swordSwipeAttack = Instantiate(swordSwipeAttackPrefab);
            // SwordSwipeAttackPhysics script = swordSwipeAttack.GetComponent<SwordSwipeAttackPhysics>();
            // script.swing = true;
            // script.startingRotationSpeed = 150;
            // if (rnum > 0.5f)
            //     script.rightSwing = true;
            // else
            //     script.rightSwing = false;
        }    
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
