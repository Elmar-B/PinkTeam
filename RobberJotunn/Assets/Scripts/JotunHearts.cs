using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class JotunHearts : MonoBehaviour
{

    public GameObject firstHeart;
    public GameObject secondHeart;
    public GameObject thirdHeart;
    private Image thirdHeartRenderer;
    private Image secondHeartRenderer;
    private Image firstHeartRenderer;
    public Sprite emptyHeart;
    public State state;

    // Start is called before the first frame update
    void Start()
    {
        thirdHeartRenderer = thirdHeart.GetComponent<Image>();
        secondHeartRenderer = secondHeart.GetComponent<Image>();
        firstHeartRenderer = firstHeart.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StateChange(int state)
    {

        if(state == 2)
        {
            thirdHeartRenderer.sprite = emptyHeart;
            return;
        }
        else if(state == 3)
        {
            secondHeartRenderer.sprite = emptyHeart;
            return;
        }
        else if(state == 4)
        {
            firstHeartRenderer.sprite = emptyHeart;
            return;
        }
    }
}
