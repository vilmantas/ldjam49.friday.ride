using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Muter : MonoBehaviour
{
    public AudioSource audioSource;
    void Start()
    {
        if (PlayerPrefs.GetInt("Muted") == 1)
        {
            audioSource.mute = true;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("m"))
        {
            if (PlayerPrefs.GetInt("Muted") == 1)
            {
                PlayerPrefs.SetInt("Muted", 0);
                audioSource.mute = false;
            }
            else
            {
                PlayerPrefs.SetInt("Muted", 1);
                audioSource.mute = true;
            }

        }

    }
}
