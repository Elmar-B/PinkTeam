using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Rigidbody2D body;
    public GameObject jotunn;
    public PlayerController playerController;
    private bool start;

    void Update()
    {
        if (transform.position.y >= 1 && !start)
        {
            body.velocity = new Vector2(0, 1);

            playerController.cutscene = true;
        }

        if (transform.position.y >= 2 && !start)
        {
            body.velocity = Vector2.zero;
            
            jotunn.SetActive(true);

            playerController.cutscene = false;
            start = true;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            body.velocity = new Vector2(0, 1);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            body.velocity = Vector2.zero;
        }
    }
}
