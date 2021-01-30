using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakdance : MonoBehaviour
{

    public float basePlateSpeed;
    public float crossSpeed;
    public float carSpeed;

    public GameObject basePlate;
    public GameObject[] crosses;
    public GameObject[] cars;


    // Update is called once per frame
    void Update()
    {
        basePlate.transform.transform.Rotate(0, Time.deltaTime * basePlateSpeed, 0, Space.Self);

        foreach (var cross in crosses)
        {
            cross.transform.transform.Rotate(0, -Time.deltaTime * crossSpeed, 0, Space.Self);
        }

        foreach (var car in cars)
        {
            car.transform.transform.Rotate(0, Time.deltaTime * carSpeed, 0, Space.Self);
        }

        //basePlate.transform.RotateAround(transform.position, transform.up, Time.deltaTime * speed);
        //basePlate.transform.Rotate(transform.up, Time.deltaTime * speed);
        //carPlate.transform.Rotate(carPlate.transform.up, Time.deltaTime * carSpeed);
    }
}
