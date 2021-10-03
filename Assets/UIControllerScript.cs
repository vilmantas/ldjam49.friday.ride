using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIControllerScript : MonoBehaviour
{
    public GameEngine Engine;

    public GameObject Scoreboard;

    public GameObject GameOverScreen;

    public GameObject StartScreen;

    public StartingScript StartingScreen;

    // Update is called once per frame
    void Update()
    {
        if (GameEngine.GameOver)
        {
            GameOverScreen.SetActive(true);

            StartingScreen.gameObject.SetActive(false);
            Scoreboard.SetActive(false);
            StartScreen.SetActive(false);

        }
        else if (GameEngine.GameStarting)
        {
            StartingScreen.gameObject.SetActive(true);

            GameOverScreen.SetActive(false);
            Scoreboard.SetActive(false);
            StartScreen.SetActive(false);
        }
        else if (GameEngine.GameStarted)
        {
            StartingScreen.Countdown.text = "Get ready!";
            Scoreboard.SetActive(true);

            GameOverScreen.SetActive(false);
            StartScreen.SetActive(false);
            StartingScreen.gameObject.SetActive(false);
        } else
        {
            StartScreen.SetActive(true);

            GameOverScreen.SetActive(false);
            Scoreboard.SetActive(false);
            StartingScreen.gameObject.SetActive(false);
        }
    }
}
