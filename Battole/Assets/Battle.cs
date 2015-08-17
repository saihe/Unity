using UnityEngine;
using System.Collections;

public class Battle : MonoBehaviour {
	public int stamina;

	// Use this for initialization
	void Start () {
		stamina = 10;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("Jump")){
			stamina -= 1;
		}
		if (stamina == 0) {
			Application.LoadLevel(Application.loadedLevel);
		}
		print (stamina);
	}
}
