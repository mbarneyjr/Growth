using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class EnemyController : MonoBehaviour {

    private float minPos, maxPos;
    private Vector3 spawnCenter;

    private Vector3 direction = new Vector3(0.0f, 0.0f, 0.0f);
    private float speed;

    public GameObject FoodManagerRef;

    private GameObject TargetFood;

    public void SetParameters(float minPosition, float maxPosition, Vector3 center, GameObject foodManager, float enemySpeed)
    {
        spawnCenter = center;
        minPos = minPosition;
        maxPos = maxPosition;
        FoodManagerRef = foodManager;
        speed = enemySpeed;
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
            posX = UnityEngine.Random.Range(-maxPos, maxPos);
            posY = UnityEngine.Random.Range(-maxPos, maxPos);
        } while ((posX < minPos && posX > -minPos) && (posY < minPos && posY > -minPos));

        Vector3 newPosition = new Vector3(posX, 1.0f, posY);
        newPosition = spawnCenter + newPosition;
        transform.position = newPosition;
    }

    // set the direction to find
    private void FindClosestFood()
    {
        float radius = 100.0f;
        List<Collider> hitColliders;
        List<Collider> foods = new List<Collider>();

        hitColliders = Physics.OverlapSphere(transform.position, radius).ToList();

        for (int i = 0; i < hitColliders.Count; i++)
        {
            if (hitColliders[i].CompareTag("Food"))
            {
                foods.Add(hitColliders[i]);
            }
        }
        if (foods.Count > 0)
        {
            float closestDistance = (foods[0].transform.position - transform.position).magnitude;
            TargetFood = foods[0].gameObject;
            for (int i = 0; i < foods.Count; i++)
            {
                if ((foods[i].transform.position - transform.position).magnitude < closestDistance)
                {
                    TargetFood = foods[i].gameObject;
                }
            }
        }
    }

    private void AddSize(float otherRadius)
    {
        float myRadius = gameObject.transform.localScale.x;
        float myNewRadius = (float)Math.Pow(Math.Pow(myRadius, 3.0f) + Math.Pow(otherRadius, 3.0f), 1.0f / 3.0f);
        gameObject.transform.localScale = new Vector3(myNewRadius, myNewRadius, myNewRadius);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Food"))
        {
            AddSize(other.transform.localScale.x);
            other.GetComponent<FoodController>().Respawn();
        }
        else if (other.CompareTag("Player") || other.CompareTag("Enemy"))
        {
            if (other.transform.localScale.x < transform.localScale.x) // Enemy will eat player/enemy
            {
                AddSize(other.transform.localScale.x);
            }
            else
            {
                Respawn();
            }
        }
        else if (other.CompareTag("Respawn"))
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
        gameObject.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
    }

    public void Update()
    {
        FindClosestFood();
        float step = speed * Time.deltaTime;
        Vector3 myPos = new Vector3(transform.position.x, 0, transform.position.z);
        Vector3 targetPos = new Vector3(TargetFood.transform.position.x, 0, TargetFood.transform.position.z);
        transform.position = Vector3.MoveTowards(myPos, targetPos, step);
    }
}
