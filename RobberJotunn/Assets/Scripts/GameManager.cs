using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Singelton
    public static GameManager instance;
    public AudioSource backgroundMusic;
    public AudioSource snowSound;
    private bool running;
    
    private void Awake ()
    {
        if (GameManager.instance == null)
            instance = this;
        else
            Destroy(gameObject);
        running = true;
    }
    
    void Update()
    {
    }

    void LateUpdate()
    {
    }

    public void GameOver()
    {
        Time.timeScale = 0;
        UIManager _ui = GetComponent<UIManager>();
        if (_ui != null && running)
        {
            running = false;
            _ui.ToggleDeathPanel();
        }
        backgroundMusic.Stop();
    }

    public void Victory()
    {
        Time.timeScale = 0;
        UIManager _ui = GetComponent<UIManager>();
        if (_ui != null && running)
        {
            running = false;
            _ui.ToggleVictoryPanel();
        }
        backgroundMusic.Stop();
    }
}
