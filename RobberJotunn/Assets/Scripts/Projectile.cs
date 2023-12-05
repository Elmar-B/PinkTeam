using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using Vector2 = UnityEngine.Vector2;
using Vector3 = UnityEngine.Vector3;

public class Projectile : MonoBehaviour
{

    private GameObject playerObj;
    private Vector3 playerPos;
    public GameObject shot;
    public float attackTime;
    private float attackTimeCounter;
    private bool attacking = false;
    
    void Start()
    {
        playerObj = GameObject.FindGameObjectWithTag("Player");
    }


    void Update()
    { 
        if(!attacking)
        {  
            playerPos = playerObj.transform.position;
            attacking = true;
            attackTimeCounter = attackTime;

            GameObject projectile = Instantiate(shot);
        }
        if (attackTimeCounter >= 0)
        {
            attackTimeCounter -= Time.deltaTime;
        }

        if (attackTimeCounter < 0)
        {
            attacking = false;
        }
    }

    


}
