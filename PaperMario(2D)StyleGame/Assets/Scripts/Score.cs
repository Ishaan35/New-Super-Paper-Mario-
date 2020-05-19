using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Text CoinCount;
    public Text LivesCount;
    public static int Coins;
    bool LevelsCompleted = false;
    bool GameOverReal = false;

    public GameObject Camera;

    public GameObject AllLevelsCleared;
    public GameObject YouLose;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        CoinCount.text = (Coins.ToString());
        LivesCount.text = (PlayerMove.Lives.ToString(""));

        

        if(LevelsCompleted == false)
        {
            if(PlayerMove.LEVEL1COMPLETE && PlayerMove.LEVEL2COMPLETE)
            {
                LevelsCompleted = true;
                //If both levels complete, do the animation for the levels complete thingy
                AllLevelsCleared.SetActive(true);
                Camera.GetComponent<AudioSource>().enabled = false;
                

            }
        }
        if (GameOverReal == false)
        {
            if (PlayerMove.Lives < 1)
            {
                GameOverReal = true;
                YouLose.SetActive(true);
                Camera.GetComponent<AudioSource>().enabled = false;

            }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            AllLevelsCleared.SetActive(false);
            Camera.GetComponent<AudioSource>().enabled = true;
        }

    }

}
