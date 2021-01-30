using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakdance : MonoBehaviour
{

    public float basePlateSpeed;
    public float crossSpeed;
    public float carSpeed;

    public float rideTime;
    private float currentRideTime;
    public float pauseTime;
    private float currentPauseTime;

    public AnimationCurve basePlateCurve;
    public AnimationCurve crossCurve;
    public AnimationCurve carCurve;

    public GameObject basePlate;
    public GameObject[] crosses;
    public GameObject[] cars;
    public float[] randomCarFq;
    public float[] randomCarOffset;

    public float basePlateMultiplier = 0;
    public float crossSpeedMultiplier = 0;
    public float carSpeedMultiplier = 0;

    public bool rideStarted = false;

    public void Awake()
    {
        randomCarFq = new float[cars.Length];
        randomCarOffset = new float[cars.Length];
        for (int i = 0; i < randomCarFq.Length; i++)
        {
            randomCarFq[i] = Random.Range(0.1f, 0.5f);
            randomCarOffset[i] = Random.Range(0f, 2.0f * Mathf.PI);
        }
    }

    public void Start()
    {
        StartRide();
    }


    public void StartRide()
    {
        Debug.Log("Starting the ride");
        rideStarted = true;
        currentRideTime = 0;
        currentPauseTime = 0;
    }

    public void EndRide()
    {
        Debug.Log("Ending the ride");

        rideStarted = false;
        currentRideTime = 0;
        currentPauseTime = 0;
    }

    private void Update()
    {
        if (rideStarted)
        {
            // Happy funfun time
            if (currentRideTime < rideTime)
            {
                currentRideTime += Time.deltaTime;
                var normalizedRideTime = currentRideTime / rideTime;
                basePlateMultiplier = basePlateCurve.Evaluate(normalizedRideTime);
                crossSpeedMultiplier = crossCurve.Evaluate(normalizedRideTime);
                carSpeedMultiplier = carCurve.Evaluate(normalizedRideTime);
            }
            // Happy funfun time over
            else
            {
                EndRide();
            }
        }
        else // Time for a little pause
        {
            currentPauseTime += Time.deltaTime;
            if (currentPauseTime > pauseTime)
            {
                StartRide();
            }
        }
    }

    void FixedUpdate()
    {
        basePlate.transform.transform.Rotate(0, Time.fixedDeltaTime * basePlateSpeed * basePlateMultiplier, 0, Space.Self);

        foreach (var cross in crosses)
        {
            cross.transform.transform.Rotate(0, Time.fixedDeltaTime * crossSpeed * crossSpeedMultiplier, 0, Space.Self);
        }

        for (int i = 0; i < cars.Length; i++)
        {
            var car = cars[i];
            float randomCarMultiplier = 10f * Mathf.Sin(Time.time + Time.time * randomCarFq[i] * 0.01f + randomCarOffset[i]);
            car.transform.transform.Rotate(0, Time.fixedDeltaTime * carSpeed * randomCarMultiplier * carSpeedMultiplier, 0, Space.Self);
        }

        //basePlate.transform.RotateAround(transform.position, transform.up, Time.deltaTime * speed);
        //basePlate.transform.Rotate(transform.up, Time.deltaTime * speed);
        //carPlate.transform.Rotate(carPlate.transform.up, Time.deltaTime * carSpeed);
    }
}
