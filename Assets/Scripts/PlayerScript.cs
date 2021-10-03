using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [Range(3, 100)]
    public float StartingSpeed = 3f;

    [Range(10, 100)]
    public float SpeedIncreaseMax = 10f;

    [Range(60, 600)]
    public float TimeToMaxSpeed = 60f;

    public ParticleSystem Particles;
    public GameObject BalanceTrigger;

    public GameEngine Engine;
    public LayerMask EndingLayer;
    public LayerMask GroundLayer;
    public Transform Head;

    [Range(1, 5)]
    public float PlayerRotationPowerAcceleration = 2f;

    [Range(1 ,5)]
    public float PlayerRotationPower;

    [Range(1, 10f)]
    public float SwingingIntervalMin = 2f;
    [Range(1, 10f)]
    public float SwingingIntervalDeviation = 5f;


    public float Arc = 45f;

    public Transform Body;

    public int RotationDireciton = 1;

    public static Quaternion rotation;

    private float directionDelta = 0f;
    private Vector3 NewTarget = Vector3.zero;

    [HideInInspector]
    public float TimeTillNextSwing = 5f;
    [HideInInspector]
    public float SwingingLeft = 0;
    [HideInInspector]
    public float RotationSpeed = 0;

    [HideInInspector]
    public float SwingingDuration = 0f;

    [HideInInspector]
    public Vector3 StartPosition = Vector3.zero;

    private Quaternion StartRotation;

    private RaycastHit HitInfo;

    private void Start()
    {
        StartRotation = transform.rotation;
        StartPosition = transform.position;
        GameEngine.MaxSpeed = StartingSpeed + SpeedIncreaseMax;
        Restart();
    }

    private void OnCollisionEnter(Collision collision)
    {
        Engine.SetGameOver($"You hit {collision.gameObject.name}");
    }


    private void Update()
    {
        Ray r = new Ray(Head.position, Vector3.forward);
        if (Physics.Raycast(r, out HitInfo, 100f, EndingLayer))
        {
            Destroy(HitInfo.collider.gameObject);
            Engine.MapGenerator.AppendTile(Engine.MapGenerator.Tile);
        }

        if (GameEngine.Pause) 
        {
            Particles.Stop();
            return;
        } else
        {
            if (!Particles.isPlaying)
            {
                Particles.Play();
            }
        }

        if (Physics.Raycast(new Ray(BalanceTrigger.transform.position, Vector3.down), out RaycastHit info, 10f, GroundLayer))
        {
            GameEngine.DistanceToGround = info.distance;
        }

        NewTarget = Quaternion.Euler(0, Arc * RotationDireciton, 0) * Vector3.forward;

        var orig = Input.GetAxisRaw("Horizontal") * PlayerRotationPowerAcceleration;
        directionDelta = orig > 1f ? 1f : orig < -1f ? -1f : orig;
        Body.rotation = Quaternion.Euler(new Vector3(0, 0, -transform.eulerAngles.y));

        SwingingLeft -= Time.deltaTime;
        TimeTillNextSwing -= Time.deltaTime;

        if (SwingingLeft < 0)
        {
            RotationSpeed = 0f;
        }

        if (TimeTillNextSwing < 0f)
        {
            RotationDireciton = new int[] { -1, 1 }[UnityEngine.Random.Range(0, 2)];

            var rotationMethod = new Action[] { SetSmallSwinging, SetMediumSwinging }[UnityEngine.Random.Range(0, 2)];

            rotationMethod();

            TimeTillNextSwing = UnityEngine.Random.Range(SwingingIntervalMin + SwingingLeft, SwingingIntervalMin + SwingingIntervalDeviation + SwingingLeft);
        }
    }

    void SetSmallSwinging()
    {
        SwingingDuration = UnityEngine.Random.Range(5, 5 + 5);
        SwingingLeft = SwingingDuration;
        RotationSpeed = UnityEngine.Random.Range(PlayerRotationPower * 0.1f, PlayerRotationPower * 0.1f + PlayerRotationPower * 0.3f);
    }

    void SetMediumSwinging()
    {
        SwingingDuration = UnityEngine.Random.Range(1f, 1f + 3f);
        SwingingLeft = SwingingDuration;
        RotationSpeed = UnityEngine.Random.Range(PlayerRotationPower * 0.4f, PlayerRotationPower * 0.4f + PlayerRotationPower * 0.2f);
    }

    void FixedUpdate()
    {
        if (GameEngine.Pause) return;

        var timeCompletion = Time.timeSinceLevelLoad / TimeToMaxSpeed;

        var increase = Mathf.Lerp(0, SpeedIncreaseMax, timeCompletion);

        GameEngine.CurrentSpeed = StartingSpeed + increase;

        transform.position += transform.forward * GameEngine.CurrentSpeed * Time.deltaTime;

        if (directionDelta != 0)
        {
            var newRotation = Vector3.RotateTowards(transform.forward, directionDelta * transform.right, (PlayerRotationPower - RotationSpeed) * Time.deltaTime, 0f);

            transform.rotation = Quaternion.LookRotation(newRotation);
        } 
        else if (SwingingLeft > 0f)
        {
            var newRotation = Vector3.RotateTowards(transform.forward, NewTarget.normalized, RotationSpeed * Time.deltaTime, 0f);

            transform.rotation = Quaternion.LookRotation(newRotation);
        }

    }

    private void OnDrawGizmos()
    {
        Ray r = new Ray(BalanceTrigger.transform.position, Vector3.down);
        if (Physics.Raycast(r, out RaycastHit info, 10f, GroundLayer))
        {
            Debug.DrawRay(BalanceTrigger.transform.position, Vector3.down * info.distance, Color.red);
        }
    }

    public void Restart()
    {
        transform.position = StartPosition;
        transform.rotation = StartRotation;
        SwingingLeft = 0f;
        RotationSpeed = 0f;
        TimeTillNextSwing = UnityEngine.Random.Range(SwingingIntervalMin + SwingingLeft, SwingingIntervalMin + SwingingIntervalDeviation + SwingingLeft);
    }
}
