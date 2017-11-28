using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodManager : MonoBehaviour {

    public GameObject EnemyManagerRef;
    public GameObject FoodRef;
    public int numberOfFood;

    List<GameObject> Foods = new List<GameObject>();

    public float minSpawn, maxSpawn;

    public List<GameObject> GetFoods()
    {
        return Foods;
    }

	// Use this for initialization
	void Start () {
        Foods = new List<GameObject>();
        for (int i = 0; i < numberOfFood; i++)
        {
            Foods.Add(Instantiate(FoodRef) as GameObject);
            Foods[i].GetComponent<FoodController>().SetParameters(minSpawn, maxSpawn, transform.position);
            Foods[i].GetComponent<FoodController>().Respawn();
        }
        EnemyManagerRef.GetComponent<EnemyManager>().CreateEnemies();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
