using UnityEngine;

public class Bullet : MonoBehaviour
{
	// 弾の移動スピード
	public int speed = 10;

	// ゲームオブジェクト生成から削除するまでの時間
	public float lifeTime = 1;

	// 攻撃力
	public int power;

    
	void Start ()
	{
		// ローカル座標のY軸方向に移動する
		GetComponent<Rigidbody2D>().velocity = transform.up.normalized * speed;
		
		// lifeTime秒後に削除
		Destroy (gameObject, lifeTime);
	}
    /*
    // 弾が表示された時に呼び出される
    void OnEnable()
    {
        // 弾の移動
        GetComponent<Rigidbody2D>().velocity = transform.up.normalized * speed;
    }

    // 弾が何らかのトリガーに当たった時に呼び出される
    void OnTriggerExit2D(Collider2D other)
    {
        // 弾の削除。実際には非アクティブにする
        ObjectPool.instance.ReleaseGameObject(gameObject);
    }
    */
    public int setPower(int p)
    {
        power = p;
        return power;
    }
}