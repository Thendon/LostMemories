using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakdance : MonoBehaviour
{

    public float speed = 1f;
    public float carSpeed = 1.5f;

    public GameObject basePlate;
    public GameObject carPlate;


    // Update is called once per frame
    void Update()
    {

        basePlate.transform.Rotate(basePlate.transform.up, Time.deltaTime * speed);
        //carPlate.transform.Rotate(carPlate.transform.up, Time.deltaTime * carSpeed);
    }
}
