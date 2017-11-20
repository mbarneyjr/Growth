using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodController : MonoBehaviour {

    private float minPos, maxPos;
    private Vector3 spawnCenter;

    public void SetParameters(float minPosition, float maxPosition, Vector3 center)
    {
        spawnCenter = center;
        minPos = minPosition;
        maxPos = maxPosition;
    }

    public void Respawn()
    {
        ResetSize();
        RandomizeColor();
        RandomizePosition();
        GetComponent<Rigidbody>().velocity = Vector3.zero;
    }

    private void RandomizePosition()
    {
        float posX = 0, posY = 0;
        do
        {
            posX = Random.Range(-maxPos, maxPos);
            posY = Random.Range(-maxPos, maxPos);
        } while ((posX < minPos && posX > -minPos) && (posY < minPos && posY > -minPos));

        Vector3 newPosition = new Vector3(posX, 1.0f, posY);
        newPosition = spawnCenter + newPosition;
        transform.position = newPosition;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Respawn"))
        {
            Respawn();
        }
    }

    private void RandomizeColor()
    {
        gameObject.GetComponent<Renderer>().material.color = new Vector4(UnityEngine.Random.Range(0.0f, 1.0f), UnityEngine.Random.Range(0.0f, 1.0f), UnityEngine.Random.Range(0.0f, 1.0f), 1.0f);
    }

    private void ResetSize()
    {
        gameObject.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
    }
}
