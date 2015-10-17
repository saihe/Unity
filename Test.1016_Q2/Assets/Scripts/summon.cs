using UnityEngine;
using System.Collections;

public class summon : MonoBehaviour {

    public GameObject sphere;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    void OnGUI()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Instantiate(sphere, sphere.transform.position, sphere.transform.rotation);
        }
    }
}
