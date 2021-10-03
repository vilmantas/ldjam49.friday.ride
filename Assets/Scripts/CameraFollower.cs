using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    public GameEngine Engine;
    public Transform player;
    private Transform CameraTransform;
    public float xOffset = 0;
    public float zOffset = 0;
    public float yOffset = 0;

    public float ShakeSpeedBase = 1f;

    public float CameraMaxShakeOffset = 0.5f;

    private float CurrentShakeOffset;

    private Vector3 ShakeDirection = Vector3.right;

    private Vector3 ShakeOffset = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        CameraTransform = GetComponent<Transform>();

    }

    // Update is called once per frame
    void Update()
    {
            CameraTransform.position = new Vector3(player.position.x + xOffset, CameraTransform.position.y, player.position.z + zOffset);
    }

    private void CheckOffset()
    {
        Vector3 moveDir;

        var offsetScale = 1 - (GameEngine.UnstableDuration / 3f);// Engine.GameOverAfterTimeUnstable;

        CurrentShakeOffset = CameraMaxShakeOffset * Mathf.Lerp(CameraMaxShakeOffset * 0.1f, CameraMaxShakeOffset, offsetScale);

        if (ShakeDirection == Vector3.right && ShakeOffset.x > CurrentShakeOffset)
        {
            ShakeDirection = -Vector3.right;
        }
        else if (ShakeDirection == -Vector3.right && ShakeOffset.x < -CurrentShakeOffset)
        {
            ShakeDirection = Vector3.right;
        }

        moveDir = ShakeDirection * ShakeSpeedBase * Time.deltaTime;

        ShakeOffset += moveDir;
    }
}
