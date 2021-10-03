using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEngine : MonoBehaviour
{
    [Range(1, 10)]
    public float GameOverAfterTimeUnstable;

    public TileGeneratorScript MapGenerator;

    public PlayerScript Player;

    public static bool Pause => _pause || GameOver;

    private static bool _pause = true;

    public bool StartImmediately = true;

    public static bool GameOver = false;

    public static float UnstableDuration = 0f;

    public static float DistanceToGround = 99f;

    public static string GameOverCause = string.Empty;

    [HideInInspector]
    public float Score = 0f;
    private void Start()
    {
        _pause = !StartImmediately;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S)) _pause = !_pause;

        if (Pause) return;

        if (DistanceToGround < 5.5)
        {
            UnstableDuration += Time.deltaTime;
        } 
        else
        {
            UnstableDuration = 0f;
        }

        if (DistanceToGround < 4 || UnstableDuration > GameOverAfterTimeUnstable)
        {
            SetGameOver("Lost balance - Unstable");
        }
    }

    public void SetGameOver(string cause)
    {
        GameOverCause = cause;
        GameOver = true;
    }
}
