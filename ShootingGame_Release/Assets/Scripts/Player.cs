using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{

    // Spaceshipコンポーネント
    Spaceship spaceship;

    //Chickコンポーネント
    public GameObject chick;

    //HP
    public int hp = 5;

    //HP GUI
    GameObject hpGUI;

    IEnumerator Start ()
    {
        // Spaceshipコンポーネントを取得
        spaceship = GetComponent<Spaceship> ();
        
        while (true) {

            // 弾をプレイヤーと同じ位置/角度で作成
            spaceship.Shot (transform);

            // ショット音を鳴らす
            GetComponent<AudioSource>().Play ();

            // shotDelay秒待つ
            yield return new WaitForSeconds (spaceship.shotDelay);
        }
    }

    void Update ()
    {
        // 右・左
        float x = Input.GetAxisRaw ("Horizontal");

        // 上・下
        float y = Input.GetAxisRaw ("Vertical");

        // 移動する向きを求める
        Vector2 direction = new Vector2 (x, y).normalized;

        // 移動の制限
        Move (direction);

        //HP GUIの監視
        chageHpGUI();
    }

    // 機体の移動
    void Move (Vector2 direction)
    {
                
        // 画面左下のワールド座標をビューポートから取得
        Vector2 min = Camera.main.ViewportToWorldPoint (new Vector2 (0, 0));

        // 画面右上のワールド座標をビューポートから取得
        Vector2 max = Camera.main.ViewportToWorldPoint (new Vector2 (1, 1));
        
        //タッチされた座標を取得
        Vector2 screenPos = Input.mousePosition;
        //print("タッチ座標: " + screenPos);

        //タッチされた座標を画面上の座標に変換
        Vector2 touchPos = Camera.main.ScreenToWorldPoint(screenPos);
        //print("画面上の座標:"  + touchPos);

        //移動量を加える
        touchPos += direction * spaceship.speed * Time.deltaTime;

        //プレイヤーの位置が画面内に収まるように制限をかける
        touchPos.x = Mathf.Clamp(touchPos.x, min.x, max.x);
        touchPos.y = Mathf.Clamp(touchPos.y, min.y, max.y);

        //制限をかけた値をプレイヤーの位置とする
        transform.position = touchPos;
                
    }

    void chageHpGUI()
    {
        //HP GUIの変更
        if(hp < 10)
        {
            hpGUI.GetComponent<GUIText>().text = "HP: " + hp.ToString();
        }
        else
        {
            hpGUI.GetComponent<GUIText>().text = "HP: MAX !!";
        }
    }

    // ぶつかった瞬間に呼び出される
    void OnTriggerEnter2D (Collider2D c)
    {
        //HP GUIの作成
        hpGUI = new GameObject();
        hpGUI = GameObject.Find("HP GUI(Clone)");

        //HP GUIの変更
        hpGUI.GetComponent<GUIText>().text = "HP: " + hp.ToString();

        //print("hp: " + hp);
        // レイヤー名を取得
        string layerName = LayerMask.LayerToName (c.gameObject.layer);
        //print("layerName: " + layerName);

        // レイヤー名がBullet (Enemy)の時は弾を削除
        if (layerName == "Bullet (Enemy)") {
            // 弾の削除
            Destroy (c.gameObject);
        }

        if(layerName == "Item")
        {
            switch (c.gameObject.name)
            {
                case "item1(Clone)":
                    //print("name: item1");
                    break;
                case "item2(Clone)":
                    //print("name: item2");
                    break;
                case "item3(Clone)":
                    //print("name: item3");
                    if(hp < 10)
                    {
                        //HP増加
                        hp++;
                    }
                    else
                    {
                        break;
                    }
                    break;
                case "item4(Clone)":
                    //print("name: item4");
                    hp = 10;
                    Instantiate(chick, chick.transform.position, chick.transform.rotation);
                    //画面上の敵全滅
                    GameObject[] enemy = GameObject.FindGameObjectsWithTag("Enemy");
                    if(enemy != null)
                    {
                        //print("Find enemy");
                        foreach(var val in enemy)
                        {
                            //Enemy削除
                            Destroy(val);
                            //爆発
                            spaceship.Explosion();
                        }
                    }else if(enemy == null)
                    {
                        print("Don't find");
                    }
                    break;
            }
        }

        // レイヤー名がBullet (Enemy)またはEnemyの場合は爆発
        if (layerName == "Bullet (Enemy)" || layerName == "Enemy")
        {
            //HPを減らす
            hp -= 1;

            //HPが0以下になったら
            if (hp <= 0)
            {
                //HP GUIの変更
                hpGUI.GetComponent<GUIText>().text = "A bone";

                // Managerコンポーネントをシーン内から探して取得し、GameOverメソッドを呼び出す
                FindObjectOfType<Manager>().GameOver();

                // 爆発する
                spaceship.Explosion();

                // プレイヤーを削除
                Destroy(gameObject);
            }
        }

    }
}
