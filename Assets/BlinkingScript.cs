using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlinkingScript : MonoBehaviour
{
    public Text Text;

    public float Interval;

    private void Start()
    {
        InvokeRepeating("ToggleText", 0f, Interval);
    }

    void ToggleText()
    {
        if (string.IsNullOrEmpty(Text.text))
        {
            Text.text = "Press ENTER to start";
        } else
        {
            Text.text = string.Empty;
        }
    }
}
