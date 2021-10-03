using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEngine : MonoBehaviour
{
    public TileGeneratorScript MapGenerator;

    public PlayerScript Player;

    public static bool Stop = true;

    public bool StartImmediately = true;

    [HideInInspector]
    public float Score = 0f;
    private void Start()
    {
        Stop = !StartImmediately;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S)) Stop = !Stop;
    }
}
