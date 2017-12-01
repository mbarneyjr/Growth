using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    public GameObject CameraRef;
    public GameObject GameMasterRef;
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
        GameMasterRef.GetComponent<GameMaster>().ResetScore();
        CameraRef.GetComponent<CameraController>().ResetDistance();
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
        CameraRef.GetComponent<CameraController>().AdjustDistance(myNewRadius / myRadius);
    }

    private void SetScore(float points)
    {
        GameMasterRef.GetComponent<GameMaster>().SetScore(points);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Food"))
        {
            AddSize(other.transform.localScale.x);
            SetScore(transform.localScale.x);
        }
        else if (other.CompareTag("Enemy"))
        {
            if (other.transform.localScale.x < transform.localScale.x) // Player will eat enemy
            {
                AddSize(other.transform.localScale.x);
                SetScore(transform.localScale.x);
            }
            else if (other.transform.localScale.x > transform.localScale.x) // enemy will eat player
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

    private void Update()
    {
        if (transform.position.y < -10.0f)
        {
            Respawn();
        }
    }
}
