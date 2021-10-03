using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindIndicatorScript : MonoBehaviour
{
    public GameObject Indicator;

    public GameEngine Engine;

    private PlayerScript Player;

    // Start is called before the first frame update
    void Start()
    {
        Player = Engine.Player;
    }

    // Update is called once per frame
    void Update()
    {
        if (Player.SwingingLeft > 0)
        {
            Indicator.SetActive(true);
            Indicator.transform.localScale = new Vector3(Engine.Player.RotationDireciton * Indicator.transform.localScale.z, Indicator.transform.localScale.y, Indicator.transform.localScale.z);

            Color c = new Color(1f * SwingPower(), 0, 0, 0.75f + (1f * SwingingLeft() / 4));

            Indicator.GetComponentInChildren<Renderer>().material.SetColor("_Color", c);
        } else
        {
            Indicator.SetActive(false);
        }
    }

    float SwingPower()
    {
        return Player.RotationSpeed / Player.PlayerRotationPower;
    }

    float SwingingLeft()
    {
        return Player.SwingingLeft / Player.SwingingDuration;
    }
}
