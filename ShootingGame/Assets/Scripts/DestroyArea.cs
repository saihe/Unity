using UnityEngine;
using System.Collections;

public class DestroyArea : MonoBehaviour {

    void OnTriggerExit2d(Collider2D c)
    {
        Destroy(c.gameObject);
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
