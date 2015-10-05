using UnityEngine;
using System.Collections;
using System;

public class Enemy : MonoBehaviour {

    //ヒットポイント
    public int hp = 1;

    //Spaceshipコンポーネント
    Spaceship spaceship;

	// Use this for initialization
	IEnumerator Start () {

        //Spaceshipコンポーネントを取得
        spaceship = GetComponent<Spaceship>();

        //ローカル座標のY軸のマイナス方向に移動する
        Move(transform.up * -1);

        //canShotがfalseの場合、ここでコルーチンを終了させる
        if(spaceship.canShot == false)
        {
            yield break;
        }

        while (true)
        {
            //子要素をすべて取得する
            for(int i = 0; i < transform.childCount; i++)
            {
                Transform shotPosition = transform.GetChild(i);

                //ShotPositionの位置/角度で弾を撃つ
                spaceship.Shot(shotPosition);
            }

            //shotDelay秒待つ
            yield return new WaitForSeconds(spaceship.shotDelay);
        }
	
	}

    void Move(Vector2 direction)
    {
        GetComponent<Rigidbody2D>().velocity = direction * spaceship.speed;
    }

    void OnTriggerEnter2D(Collider2D c)
    {
        //レイヤー名を取得
        string layerName = LayerMask.LayerToName(c.gameObject.layer);

        //レイヤー名がBullet(Player)位階のときは何も行わない
        if (layerName != "Bullet(Player)") return;

        //PlayerBulletのTransformを種痘k
        Transform playerBulletTransform = c.transform.parent;

        //Bulletのコンポーネントを取得
        Bullet bullet = playerBulletTransform.GetComponent<Bullet>();

        //ヒットポイントを減らす
        hp = hp - bullet.power;

        //弾の削除
        Destroy(c.gameObject);

        //ヒットポイントが0以下であれば
        if (hp <= 0) {
            //爆発
            spaceship.Explosion();

            //エネミーの削除
            Destroy(gameObject);
        }
        else
        {
            spaceship.GetAnimator().SetTrigger("Damage");
        }
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
