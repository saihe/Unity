﻿using UnityEngine;
using System.Collections;
using UnitySampleAssets.CrossPlatformInput;

public class Player : MonoBehaviour {

    //Spaceshipコンポーネント
    Spaceship spaceship;

    //Backgroundコンポーネント
    Background background;

    // Use this for initialization
    IEnumerator Start ()
    {
        //Spaceshipコンポーネントを取得
        spaceship = GetComponent<Spaceship>();

        //Backgroundコンポーネントを取得。3つのうちどれか1つを取得する
        background = FindObjectOfType<Background>();

        while (true) {
            //弾をプレイヤーと同じ位置/角度で作成
            spaceship.Shot(transform);

            //ショット音を鳴らす
            GetComponent<AudioSource>().Play();

            //shotDelay秒待つ
            yield return new WaitForSeconds(spaceship.shotDelay);
        }
    }
	
	// Update is called once per frame
	void Update () {
        
        //右・左
        float x = CrossPlatformInputManager.GetAxisRaw("Horizontal");

        //上。下
        float y = CrossPlatformInputManager.GetAxisRaw("Vertical");
        if(x < 0.5)
        {
            x = x - 1;
        }else if(x > 0.5 && x < 0.6)
        {
            x = 0;
        }
        if(y < 0.6)
        {
            y = y + -1;
        }else if(y > 0.5 && y< 0.6)
        {
            y = 0;
        }
        print("x: " + x + " y: " + y);
        /*
        //右・左
        float x = Input.GetAxisRaw("Horizontal");

        //上。下
        float y = Input.GetAxisRaw("Vertical");
        print("x: " + x + " y: " + y);
        */

        //移動する向きを求める
        Vector2 direction = new Vector2(x, y).normalized;

        //移動
        //spaceship.Move(direction);

        //移動の制限
        Move(direction);
	
	}

    void Move(Vector2 direction)
    {
        /*
        //背景のスケール
        Vector2 scale = background.transform.localScale;

        //背景のスケールから取得
        Vector2 min = scale * -0.5f;

        //背景のスケールから取得
        Vector2 max = scale * 0.5f;
        */
        
        //画面左下のワールド座標をビューポートから取得
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));

        //画面右上のワールド座標をビューポートから取得
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
        
        //プレイヤーの座標を取得
        Vector2 pos = transform.position;

        //移動量を加える
        pos += direction * spaceship.speed * Time.deltaTime;

        //プレイヤーの位置が画面内に収まるように制限をかける
        pos.x = Mathf.Clamp(pos.x, min.x, max.x);
        pos.y = Mathf.Clamp(pos.y, min.y, max.y);

        //制限をかけた値をプレイヤーの位置とす
        transform.position = pos;


        /*
        //タッチされた座標を取得
        Vector2 screenPos = Input.mousePosition;

        //タッチされた座標を画面上の座標に変換
        Vector2 touchPos = Camera.main.ScreenToWorldPoint(screenPos);

        //移動量を加える
        touchPos += direction * spaceship.speed * Time.deltaTime;

        //プレイヤーの位置が画面内に収まるように制限をかける
        touchPos.x = Mathf.Clamp(touchPos.x, min.x, max.x);
        touchPos.y = Mathf.Clamp(touchPos.y, min.y, max.y);

        //制限をかけた値をプレイヤーの位置とする
        transform.position = touchPos;
        */
    }

    //ぶつかった瞬間呼び出される
    void OnTriggerEnter2D(Collider2D c)
    {

        //レイヤー名を取得
        string layerName = LayerMask.LayerToName(c.gameObject.layer);
        print(layerName + " to Player");

        //レイヤー名がBullet(Enemy)のときは弾を削除
        if(layerName == "Bullet(Enemy)")
        {
            //弾の削除
            Destroy(c.gameObject);

        }

        //レイヤー名がBUllet(Enemy)またはEnemyの場合は爆発
        if (layerName == "Bullet(Enemy)" || layerName == "Enemy")
        {
            //Managerコンポートネントからシーン内から探して取得し、GameOverメソッドを呼び出す
            FindObjectOfType<Manager>().GameOver();

            //爆発する
            spaceship.Explosion();

            //プレイヤーを削除
            Destroy(gameObject);
        }


    }
}
