using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class IntroScript : MonoBehaviour
{
    public Image Cover;

    public float FadeInTime = 2f;

    public GameObject Canvas;

    public Transform Sun;

    private void Start()
    {
        Canvas.SetActive(true);
        Cover.color = new Color(Cover.color.r, Cover.color.g, Cover.color.b, 1);
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeSinceLevelLoad < 0.2f) return;

        var t = Time.timeSinceLevelLoad % 60f / 60f;

        Debug.Log(t);

        Sun.rotation = Quaternion.Euler(new Vector3(Sun.rotation.eulerAngles.x, t * 90, Sun.rotation.eulerAngles.z));

        var alpha = Mathf.Lerp(1f, 0f, (Time.timeSinceLevelLoad - 0.2f) / FadeInTime );

        Cover.color = new Color(Cover.color.r, Cover.color.g, Cover.color.b, alpha);

        if (Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene("MainScene");
        }
    }
}
