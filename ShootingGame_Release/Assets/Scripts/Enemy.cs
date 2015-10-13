using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
	// ヒットポイント
	public int hp = 1;

	// スコアのポイント
	public int point = 100;

	// Spaceshipコンポーネント
	Spaceship spaceship;

    //Itemsプレハブ
    public GameObject[] items;

    //Itemを持ってるかどうか
    public bool haveItem = false;

    //Itemを持っているかどうかランダム
    public int haveRandom = 3;
    
	IEnumerator Start ()
	{
        // Spaceshipコンポーネントを取得
        spaceship = GetComponent<Spaceship> ();

       	// ローカル座標のY軸のマイナス方向に移動する
		Move (transform.up * -1);
        print("EnemyHP: " + hp);
        // canShotがfalseの場合、ここでコルーチンを終了させる
        if (spaceship.canShot == false) {
			yield break;
		}
            
        while (true) {
			// 子要素を全て取得する
			for (int i = 0; i < transform.childCount; i++) {
				Transform shotPosition = transform.GetChild (i);
				// ShotPositionの位置/角度で弾を撃つ
				spaceship.Shot (shotPosition);
			}
			// shotDelay秒待つ
			yield return new WaitForSeconds (spaceship.shotDelay);
		}
	}

    // 機体の移動
    public void Move (Vector2 direction)
	{
		GetComponent<Rigidbody2D>().velocity = direction * spaceship.speed;
	}

	void OnTriggerEnter2D (Collider2D c)
	{
 
		// レイヤー名を取得
		string layerName = LayerMask.LayerToName (c.gameObject.layer);

        // レイヤー名がBullet (Player)以外の時は何も行わない
        if (layerName != "Bullet (Player)")
        {
            return;
        }

		// PlayerBulletのTransformを取得
		Transform playerBulletTransform = c.transform.parent;

		// Bulletコンポーネントを取得
		Bullet bullet =  playerBulletTransform.GetComponent<Bullet>();

		// ヒットポイントを減らす
		hp = hp - bullet.power;

		// 弾の削除
		Destroy(c.gameObject);

		// ヒットポイントが0以下であれば
		if(hp <= 0 )
		{
            //ランダムでアイテムを持っているか判定
            haveRandom = Random.Range(1, 10);
            if(haveRandom > 0 && haveRandom < 4)
            {
                //Itemを持っている
                haveItem = true;
            }
            else
            {
                //Itemを持っていない
                haveItem = false;
            }
            //haveRandom無効
            haveItem = true;
            //print("haveItem: " + haveItem);

			// スコアコンポーネントを取得してポイントを追加
			FindObjectOfType<Score>().AddPoint(point);

			// 爆発
			spaceship.Explosion ();
            
            // エネミーの削除
            Destroy (gameObject);

            if (haveItem == true)
            {
                int num = Random.Range(0, items.Length);
                //Itemをインスタンス
                Instantiate(items[3], gameObject.transform.position, items[num].transform.rotation);
            }
        }
        else{
			// Damageトリガーをセット
			spaceship.GetAnimator().SetTrigger("Damage");
		}
	}

    public int setHp(int level)
    {
        print("Input Level: " + level);
        hp = level * hp;
        hp = 1000;
        print("currentHP: " + hp);
        return hp;
    }
}