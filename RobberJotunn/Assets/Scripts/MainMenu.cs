using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // When Player presses PLAY Button
    public void PlayGame(){
        // Load next Scene
        SceneManager.LoadScene("MainScene");
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
}
