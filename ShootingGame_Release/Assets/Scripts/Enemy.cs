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
        //print("bullet.power: " + bullet.power );
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
            //print("haveItem: " + haveItem);

			// スコアコンポーネントを取得してポイントを追加
			FindObjectOfType<Score>().AddPoint(point);

			// 爆発
			spaceship.Explosion ();

            // エネミーの削除して元の位置に戻す
            //Destroy (gameObject);
            gameObject.SetActive(false);
            Emitter emitter = FindObjectOfType<Emitter>();
            emitter.setActive();
            gameObject.transform.position = gameObject.transform.position;
            //haveRandom無効
            //haveItem = true;

            //Itemを作るかどうか
            if (haveItem == true)
            {
                int itemRan = Random.Range(0, 100);
                int num = 0;
                // 0-34 = 35%
                if (itemRan < 35) 
                {
                    num = 0;
                }
                //35-50 = 16%
                else if(itemRan > 34 && itemRan < 51)
                {
                    num = 1;
                }
                //51-77 = 27%
                else if(itemRan > 50 && itemRan < 78)
                {
                    num = 2;
                }
                //78-99 = 22%
                else if(itemRan > 77 && itemRan < 100)
                {
                    num = 3;
                }

                //Itemをインスタンス
                Instantiate(items[num], gameObject.transform.position, items[num].transform.rotation);
            }
        }
        else{
			// Damageトリガーをセット
			spaceship.GetAnimator().SetTrigger("Damage");
		}
	}

    //HPを変更
    public int setHp(int level)
    {
        print("Input Level: " + level);
        hp = level * hp;
        //hp = 1000;
        print("currentHP: " + hp);
        return hp;
    }

    //Speedを変更
    public float setSpeed(int level)
    {
        spaceship = GetComponent<Spaceship>();
        print("Current Speed: " + spaceship.speed);
        float speedUp = 1.0f + (level * 0.4f);
        print("Speed UP: *" + speedUp);
        spaceship.speed = spaceship.speed * speedUp;
        return spaceship.speed;
    }

    //Speedを変更
    public float setDelay(int level)
    {
        spaceship = GetComponent<Spaceship>();
        print("Current Speed: " + spaceship.speed);
        float speedUp = level * 0.9f;
        print("Speed UP: *" + speedUp);
        spaceship.shotDelay = spaceship.shotDelay * speedUp;
        return spaceship.shotDelay;
    }

}