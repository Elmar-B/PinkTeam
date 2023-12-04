using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public int health;
    public Rigidbody2D body;
    private float horizontalMove;
    private float verticalMove;
    private bool attack;
    private bool dodge;

    void Update()
    {
        horizontalMove = Input.GetAxis("Horizontal");
        verticalMove = Input.GetAxis("Vertical");

        attack = Input.GetMouseButtonDown(0);
        dodge = Input.GetKeyDown(KeyCode.Space);
    }
    void FixedUpdate()
    {
        body.velocity = new Vector2(horizontalMove * speed, verticalMove * speed);
    }
}
