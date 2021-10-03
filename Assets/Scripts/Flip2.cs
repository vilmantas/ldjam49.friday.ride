using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flip2 : MonoBehaviour
{
    public float speed = 100;
    void Start()
    {
        InvokeRepeating("flip", 0, speed);
    }

    public void flip()
    {
        transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
    }
}
