using UnityEngine;
using System.Collections;

public class DestroyArea : MonoBehaviour {

    void OnTriggerExit2D(Collider2D c)
    {
        print(c + "is out of DestroyArea");
        Destroy(c.gameObject);
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
