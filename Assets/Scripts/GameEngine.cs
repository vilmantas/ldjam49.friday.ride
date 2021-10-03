using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEngine : MonoBehaviour
{
    [Range(1, 10)]
    public float StartDelay = 3f;

    [Range(1, 10)]
    public float GameOverAfterTimeUnstable;

    public TileGeneratorScript MapGenerator;

    public PlayerScript Player;

    public static bool Pause => _pause || GameOver || !GameStarted || GameStarting;

    private static bool _pause = true;

    public bool StartImmediately = true;

    public static bool GameOver = false;

    public static bool GameStarted = false;

    public static bool GameStarting => GameStartDelay > 0 && GameStarted;

    public static float GameStartDelay = 3f;

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
        if (Input.GetKeyDown(KeyCode.Space)) GameStarted = true;

        GameStartDelay -= Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space)) Restart();

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

    public void Restart()
    {
        GameOver = false;
        GameOverCause = string.Empty;
        GameStartDelay = StartDelay;
        DistanceToGround = 99f;
        UnstableDuration = 0f;
        var scene = SceneManager.GetActiveScene(); SceneManager.LoadScene(scene.name);

    }

    public void SetGameOver(string cause)
    {
        GameOverCause = cause;
        GameOver = true;
    }
}
