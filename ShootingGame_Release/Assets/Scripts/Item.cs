using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour {

    // Spaceshipコンポーネント
    Spaceship spaceship;

    // スコアのポイント
    public int point = 100;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

    }
    // 移動
    public void Move(Vector2 direction)
    {
        GetComponent<Rigidbody2D>().velocity = direction * spaceship.speed;
    }

    void OnTriggerEnter2D(Collider2D c)
    {
        // レイヤー名を取得
        string layerName = LayerMask.LayerToName(c.gameObject.layer);
        print(layerName);
        // レイヤー名がPlayer以外の時は何も行わない
        if (layerName != "Player") return;

        // PlayerのTransformを取得
        Transform playerTransform = c.transform.parent;

        // スコアコンポーネントを取得してポイントを追加
        FindObjectOfType<Score>().AddPoint(point);

        // アイテムの削除
        Destroy(gameObject);

    }
}
