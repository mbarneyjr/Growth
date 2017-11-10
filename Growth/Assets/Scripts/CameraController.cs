﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public GameObject playerRef;

    public Vector3 offset;

    // Use this for initialization
    void Start()
    {
        offset = transform.position - playerRef.GetComponent<Rigidbody>().position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = playerRef.transform.position + offset;
    }
}
