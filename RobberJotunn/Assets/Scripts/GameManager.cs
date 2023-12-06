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
        Debug.Log("Hello");
        Time.timeScale = 0;
        Debug.Log("Hi");
        UIManager _ui = GetComponent<UIManager>();
        if (_ui != null)
        {
            _ui.ToggleDeathPanel();
        }
    }
}
