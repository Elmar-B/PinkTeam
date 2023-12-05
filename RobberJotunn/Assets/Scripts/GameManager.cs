using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject player;
    private PlayerController playerScript;
    private bool gameRunning;

    void Start()
    {
        gameRunning = true;
        playerScript = player.GetComponent<PlayerController>();
    }

    void Update()
    {
        if (playerScript.health <= 0)
            gameRunning = false;
    }

    void LateUpdate()
    {
        if (!gameRunning)
        {
            ResetGame();
        }
    }

    void ResetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
