using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour
{

    public float dayLength;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        float rotationSpeed = Time.deltaTime / dayLength;
        transform.Rotate(0, rotationSpeed, 0);
    }
}
