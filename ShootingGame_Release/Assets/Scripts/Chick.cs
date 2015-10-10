using UnityEngine;
using System.Collections;

public class Chick : MonoBehaviour {

    public int speed;
        
    //ひよこを作成し画面いっぱいに右から左へ流す
	// Use this for initialization
	void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        // ローカル座標のX軸のマイナス方向に移動する
        Move(transform.right * -1);
    }

    // 機体の移動
    public void Move(Vector2 direction)
    {
        GetComponent<Rigidbody2D>().velocity = direction * speed;
    }
}
