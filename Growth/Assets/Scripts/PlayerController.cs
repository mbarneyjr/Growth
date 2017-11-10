using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    public float speed;

    // Use this for initialization
    void Start()
    {
        Respawn();
    }

    public void Respawn()
    {
        gameObject.transform.localScale = new Vector3(1, 1, 1);
        gameObject.GetComponent<Renderer>().material.color = new Vector4(UnityEngine.Random.Range(0.0f, 1.0f), UnityEngine.Random.Range(0.0f, 1.0f), UnityEngine.Random.Range(0.0f, 1.0f), 1.0f);
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        transform.position = new Vector3(0.0f, 1.0f, 0.0f);
    }

    private void AddSize(float otherRadius)
    {
        double otherVolume = 4.0f * Math.PI * Math.Pow(otherRadius, 3) / 3;
        float myRadius = gameObject.transform.localScale.x;
        double myVolume = 4.0f * Math.PI * Math.Pow(myRadius, 3) / 3;
        double myNewVolume = myVolume + otherVolume;
        double myNewRadius = Math.Pow((3 * myNewVolume) / (4 * Math.PI), 1.0 / 3.0);
        gameObject.transform.localScale = new Vector3((float)myNewRadius, (float)myNewRadius, (float)myNewRadius);

    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        GetComponent<Rigidbody>().AddForce(movement * speed);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
