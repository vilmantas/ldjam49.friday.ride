using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour
{
    public GameEngine Engine;

    public Text ScoreText;

    public Text SpeedText;

    private void Update()
    {
        ScoreText.text = Score().ToString("0.0");
        Engine.Score = Score();

        SpeedText.text = $"{GameEngine.CurrentSpeed.ToString("00")} / {GameEngine.MaxSpeed.ToString("00")}";
    }

    [HideInInspector]
    public float Score()
    {
        return (Engine.Player.transform.position.z - Engine.Player.StartPosition.z) / 5;
    }
}
