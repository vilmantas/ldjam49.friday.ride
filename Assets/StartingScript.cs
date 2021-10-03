using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartingScript : MonoBehaviour
{
    public Text Countdown;

    private int OriginalFontSize;

    void Start()
    {
        OriginalFontSize = Countdown.fontSize;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameEngine.GameStartDelay > 2.999f)
        {
            Countdown.fontSize = OriginalFontSize;
            Countdown.text = "Get ready!";
            return;
        }
        var delay = Mathf.Floor(GameEngine.GameStartDelay + 1);

        var scaler = 1 - (GameEngine.GameStartDelay - (float)System.Math.Truncate(GameEngine.GameStartDelay));

        Countdown.fontSize = (int)Mathf.Round(OriginalFontSize * scaler * 1.2f);

        Countdown.text = delay <= 1 ? 1.ToString("0") : delay.ToString("0");
    }
}
