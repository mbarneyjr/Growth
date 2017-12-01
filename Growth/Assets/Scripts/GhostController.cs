using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostController : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    public void CopyAttrubutes(Vector3 scale, Vector3 position, Vector4 color)
    {
        GetComponent<Renderer>().material.color = new Vector4(color.x, color.y, color.z, GetComponent<Renderer>().material.color.a);
        GetComponent<Transform>().localScale = scale;
        GetComponent<Transform>().position = position;
    }
	
	// Update is called once per frame
	void Update () {
        transform.Translate(new Vector3(0.0f, 1.0f, 0.0f) * Time.deltaTime);
        if (transform.position.y > 30.0f)
        {
            Destroy(gameObject);
        }
	}
}
