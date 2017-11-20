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
        ResetSize();
        RandomizeColor();
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        transform.position = new Vector3(0.0f, 1.0f, 0.0f);
    }

    private void RandomizeColor()
    {
        gameObject.GetComponent<Renderer>().material.color = new Vector4(UnityEngine.Random.Range(0.0f, 1.0f), UnityEngine.Random.Range(0.0f, 1.0f), UnityEngine.Random.Range(0.0f, 1.0f), 1.0f);
    }

    private void ResetSize()
    {
        gameObject.transform.localScale = new Vector3(1, 1, 1);
    }

    private void AddSize(float otherRadius)
    {
        float myRadius = gameObject.transform.localScale.x;
        float myNewRadius = (float)Math.Pow(Math.Pow(myRadius, 3.0f) + Math.Pow(otherRadius, 3.0f), 1.0f / 3.0f);
        gameObject.transform.localScale = new Vector3(myNewRadius, myNewRadius, myNewRadius);
        gameObject.transform.position = new Vector3(transform.position.x, transform.position.y + (myNewRadius - myRadius), transform.position.z);
        Debug.Log("New radius: " + myNewRadius);
        Debug.Log("Setting y to: " + transform.position.y);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Food"))
        {
            AddSize(other.transform.localScale.x);
            other.GetComponent<FoodController>().Respawn();
        }
        else if (other.CompareTag("Enemy"))
        {
            if (other.transform.localScale.x < transform.localScale.x) // Player will eat enemy
            {
                AddSize(other.transform.localScale.x);
            }
            else
            {
                Respawn();
            }
        }
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
