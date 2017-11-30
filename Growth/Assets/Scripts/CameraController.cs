using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject playerRef;
    public Vector3 offset;
    private float baseMagnitude;

    // Use this for initialization
    void Start()
    {
        offset = transform.position - playerRef.GetComponent<Rigidbody>().position;
        baseMagnitude = offset.magnitude;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = playerRef.transform.position + offset;
    }

    public void ResetDistance()
    {
        offset = offset.normalized * baseMagnitude;
    }

    public void AdjustDistance(float proportion)
    {
        float difference = proportion - 1.0f;
        difference /= 2.0f;
        proportion = 1.0f + difference;
        float newMagnitude = offset.magnitude * proportion;
        offset = offset.normalized * newMagnitude;
    }
}
