using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Rigidbody2D body;
    public GameObject jotunn;
    public GameObject puff;
    public PlayerController playerController;
    private bool start;
    private bool done;

    void FixedUpdate()
    {
        if (done)
        {
            body.velocity = Vector2.zero;
            return;
        }

        if (!start && transform.position.y >= 1)
        {
            body.velocity = new Vector2(0, 1);

            playerController.cutscene = true;
        }

        if (!start && transform.position.y >= 2.5)
        {
            body.velocity = Vector2.zero;
            puff.SetActive(true);
            
            jotunn.SetActive(true);

            StartCoroutine(Begin());
        }

        else if (start && transform.position.y <= 2)
        {
            body.velocity = Vector2.zero;
                        
            playerController.cutscene = false;
            jotunn.GetComponent<JotunnController>().FirstAttack();

            done = true;
        }
    }

    IEnumerator Begin()
    {
        yield return new WaitForSeconds(2);

        body.velocity = new Vector2(0, -1);

        start = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && !done)
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
