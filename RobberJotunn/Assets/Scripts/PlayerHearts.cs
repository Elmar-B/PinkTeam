using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHearts : MonoBehaviour
{
    public int health;
    public int numOfhearts;

    public Image[] hearts;
    public Sprite fullheart;
    //Optinal
    public Sprite emptyHeart;

    public void Update(){

        if (health > numOfhearts){
            health = numOfhearts;
        }

        for(int i = 0; i < hearts.Length; i++){
            if(i < health){
                hearts[i].sprite = fullheart;
            } else{
                hearts[i].sprite = emptyHeart;
            }

            if (i < numOfhearts){
                hearts[i].enabled = true;
            }else{
                hearts[i].enabled = false;
            }
        }
    }
}
