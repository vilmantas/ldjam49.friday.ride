using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIControllerScript : MonoBehaviour
{
    public GameEngine Engine;

    public GameObject Scoreboard;

    public GameObject GameOverScreen;

    // Update is called once per frame
    void Update()
    {
        if (GameEngine.GameOver)
        {
            GameOverScreen.SetActive(true);
            Scoreboard.SetActive(false);
        } else
        {
            GameOverScreen.SetActive(false);
            Scoreboard.SetActive(true);
        }
    }
}
