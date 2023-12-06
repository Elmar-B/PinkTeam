using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StateManager : MonoBehaviour
{

    public void ResetGame()
    {
        Debug.Log("Resetting Game");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1;
    }

    public void ChangeSceneByName(string name)
    {
        if (name != null)
           SceneManager.LoadScene(name);
    }
}
