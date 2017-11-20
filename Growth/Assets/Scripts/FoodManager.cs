using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoodManager : MonoBehaviour {

    public GameObject FoodRef;
    public int numberOfFood;

    List<GameObject> Foods = new List<GameObject>();

    public float minSpawn, maxSpawn;

    public List<Vector3> GetFoods()
    {
        List<Vector3> positions = new List<Vector3>();
        for (int i = 0; i < numberOfFood; i++)
        {
            positions.Add(Foods[i].transform.position);
        }
        return positions;
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
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
