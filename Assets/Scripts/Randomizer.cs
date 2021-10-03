using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Randomizer : MonoBehaviour
{
    public float DisableProbability = 0.5f;
    void Start()
    {
        if(Random.Range(0f,1f) < DisableProbability)
        {
            gameObject.SetActive(false);
        }
    }
}
