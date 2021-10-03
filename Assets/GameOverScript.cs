using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScript : MonoBehaviour
{
    public GameEngine Engine;

    public Text CauseText;

    public Text ScoreText;

    private void Start()
    {
        CauseText.text = GameEngine.GameOverCause;
        ScoreText.text = Engine.Score.ToString("0.00");

    }
}
