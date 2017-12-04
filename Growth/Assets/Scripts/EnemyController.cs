﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using System;
using System.Linq;

public class EnemyController : MonoBehaviour {

    public float minPos, maxPos;
    public Vector3 spawnCenter;
    public float baseSize;

    private Vector3 direction = new Vector3(0.0f, 0.0f, 0.0f);
    public float speed;

    public GameObject FoodManagerRef;
    public GameObject GhostRef;
    private List<GameObject> Foods;
    private int TargetFood = -1;

    private void Start()
    {
        InvokeRepeating("RemoveMass",1.0f, 5.0f);
    }

    public void SetParameters(float minPosition, float maxPosition, Vector3 center, GameObject foodManager, float enemySpeed, float size, GameObject ghost)
    {
        spawnCenter = center;
        minPos = minPosition;
        maxPos = maxPosition;
        FoodManagerRef = foodManager;
        speed = enemySpeed;
        baseSize = size;
        GhostRef = ghost;
    }

    public void OnFoodEaten()
    {
        NavigateToClosestFood();
    }

    public void SpawnGhost()
    {
        GameObject ghost = Instantiate(GhostRef);
        ghost.GetComponent<GhostController>().CopyAttrubutes(transform.localScale, transform.position, gameObject.GetComponent<Renderer>().material.color);
    }

    public void Respawn()
    {
        ResetSize();
        RandomizeColor();
        RandomizePosition();
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        NavigateToClosestFood();
    }

    private void RandomizePosition()
    {
        float posX = 0, posY = 0;
        do
        {
            posX = UnityEngine.Random.Range(-maxPos, maxPos);
            posY = UnityEngine.Random.Range(-maxPos, maxPos);
        } while ((posX < minPos && posX > -minPos) && (posY < minPos && posY > -minPos));

        Vector3 newPosition = new Vector3(posX, baseSize / 2.0f, posY);
        newPosition = spawnCenter + newPosition;
        transform.position = newPosition;
    }

    // set the direction to find
    private void NavigateToClosestFood()
    {
        TargetFood = -1;
        Foods = FoodManagerRef.GetComponent<FoodManager>().GetFoods();
        float closestDistance = Mathf.Infinity;

        for (int i = 0; i < Foods.Count; i++)
        {
            float distance = Vector3.Distance(Foods[i].transform.position, transform.position);
            if (distance < closestDistance)
            {
                TargetFood = i;
                closestDistance = distance;
            }
        }
        // navigate to the closest food
        GetComponent<NavMeshAgent>().SetDestination(Foods[TargetFood].transform.position);
        GetComponent<NavMeshAgent>().speed = speed;
        // subscribe closest food target
        Foods[TargetFood].GetComponent<FoodController>().SubscribeEnemy(gameObject);
    }

    private void AddSize(float otherRadius)
    {
        float myRadius = gameObject.transform.localScale.x;
        float myNewRadius = (float)Math.Pow(Math.Pow(myRadius, 3.0f) + Math.Pow(otherRadius, 3.0f), 1.0f / 3.0f);
        gameObject.transform.localScale = new Vector3(myNewRadius, myNewRadius, myNewRadius);
        gameObject.transform.position = new Vector3(transform.position.x, transform.position.y + (myNewRadius - myRadius), transform.position.z);
    }

    private void RemoveMass()
    {
        if (gameObject.transform.localScale.x > baseSize)
        {
            Debug.Log("Removing mass");
            AddSize(-0.5f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Food"))
        {
            AddSize(other.transform.localScale.x);
        }
        else if (other.CompareTag("Player") || other.CompareTag("Enemy"))
        {
            if (other.transform.localScale.x < transform.localScale.x) // Enemy will eat player/enemy
            {
                AddSize(other.transform.localScale.x);
            }
            else if (other.transform.localScale.x > transform.localScale.x)
            {
                SpawnGhost();
                Respawn();
            }
        }
        else if (other.CompareTag("Respawn"))
        {
            Respawn();
        }
        GetComponent<Rigidbody>().velocity = Vector3.zero;
    }

    private void RandomizeColor()
    {
        gameObject.GetComponent<Renderer>().material.color = new Vector4(UnityEngine.Random.Range(0.0f, 1.0f), UnityEngine.Random.Range(0.0f, 1.0f), UnityEngine.Random.Range(0.0f, 1.0f), 1.0f);
    }

    private void ResetSize()
    {
        gameObject.transform.localScale = new Vector3(baseSize, baseSize, baseSize);
    }

    private void Update()
    {
        if (transform.position.y < -10.0f)
        {
            Respawn();
        }
    }
}
