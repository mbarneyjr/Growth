using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {
    public GameObject EnemyRef;
    public int numberOfEnemies;

    List<GameObject> Enemies = new List<GameObject>();

    public float minSpawn, maxSpawn;

    public GameObject FoodManagerRef;
    public float enemySpeed;

    // Use this for initialization
    void Start()
    {
        Enemies = new List<GameObject>();
        for (int i = 0; i < numberOfEnemies; i++)
        {
            Enemies.Add(Instantiate(EnemyRef) as GameObject);
            Enemies[i].GetComponent<EnemyController>().SetParameters(minSpawn, maxSpawn, transform.position, FoodManagerRef, enemySpeed);
            Enemies[i].GetComponent<EnemyController>().Respawn();
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
