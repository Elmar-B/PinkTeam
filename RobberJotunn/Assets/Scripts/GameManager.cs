using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Singelton
    public static GameManager instance;
    
    private void Awake ()
    {
        if (GameManager.instance == null)
            instance = this;
        else
            Destroy(gameObject);
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
        if (_ui != null)
        {
            _ui.ToggleDeathPanel();
        }
    }

    public void Victory()
    {
        Time.timeScale = 0;
        UIManager _ui = GetComponent<UIManager>();
        if (_ui != null)
        {
            _ui.ToggleVictoryPanel();
        }
    }
}
