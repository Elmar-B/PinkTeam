using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Singelton
    public static GameManager instance;
    public AudioSource BackgroundMusic;
    private bool running;
    
    private void Awake ()
    {
        if (GameManager.instance == null)
            instance = this;
        else
            Destroy(gameObject);
        running = true;
    }

    void Start()
    {
        BackgroundMusic.Play();
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
        BackgroundMusic.Stop();
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
        BackgroundMusic.Stop();
    }
}
