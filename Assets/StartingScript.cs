using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartingScript : MonoBehaviour
{
    public Text Countdown;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GameEngine.GameStartDelay > 2.999f)
        {
            Countdown.text = "Get ready!";
            return;
        }
        var delay = Mathf.Floor(GameEngine.GameStartDelay + 1);
        Countdown.text = delay <= 1 ? 1.ToString("0") : delay.ToString("0");
    }
}
