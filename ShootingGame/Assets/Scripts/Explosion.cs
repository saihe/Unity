using UnityEngine;
using System.Collections;

public class Explosion : MonoBehaviour {

    void OnAnimationFinish()
    {
        Destroy(gameObject);
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
