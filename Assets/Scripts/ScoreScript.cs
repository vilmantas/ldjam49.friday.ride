using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScript : MonoBehaviour
{
    public GameEngine Engine;

    public Text ScoreText;

    private void Update()
    {
        ScoreText.text = Score().ToString("0.0");
        Engine.Score = Score();
    }

    [HideInInspector]
    public float Score()
    {
        return (Engine.Player.transform.position.z - Engine.Player.StartPosition.z) / 5;
    }
}
