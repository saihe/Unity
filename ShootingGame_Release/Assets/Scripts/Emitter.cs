using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Emitter : MonoBehaviour
{
	// Waveプレハブを格納する
	public GameObject[] waves;
	
	// 現在のWave
	public int currentWave { get; private set; }
	
	// Managerコンポーネント
	private Manager manager;

    //Enemyのアクティブ監視
    public int checkActive;

    //Emitterの子要素wave
    GameObject g;

    //リスト
    List<GameObject> wave;

    //現在のwave
    GameObject nowWave;

    //現在のwaveのカウント
    int nowCount;



    public int setActive()
    {
        checkActive++;
        print("checkActive: " + checkActive);
        return checkActive;
    }

    IEnumerator Start ()
	{
		
		// Waveが存在しなければコルーチンを終了する
		if (waves.Length == 0) {
			yield break;
		}
		
		// Managerコンポーネントをシーン内から探して取得する
		manager = FindObjectOfType<Manager>();

		while (true) {
            
            // タイトル表示中は待機
            while (manager.IsPlaying() == false)
            {
                yield return new WaitForEndOfFrame();
            }
            
            //waveを作成し非アクティブにしてゲーム開始
            if(transform.childCount == 0)
            {
                nowCount = 0;
                wave = new List<GameObject>();
                nowWave = new GameObject();
                int v = 0;
                foreach (var val in waves)
                {
                    // Waveを作成する
                    g = (GameObject)Instantiate(val, transform.position, Quaternion.identity);
                    
                    //リストに追加
                    wave.Add(g);
                    print("wave: " + wave[v]);
                    v++;
                    // WaveをEmitterの子要素にする
                    g.transform.parent = transform;

                    //waveの非表示
                    g.SetActive(false);
                }
                print("start: " + manager.getStart());
                manager.GameStart();
            }

            //Enemyを配列に格納
            Enemy[] enemys = FindObjectsOfType<Enemy>();
            //Enemyの設定
            if (enemys != null)
            {
                foreach (var val in enemys)
                {
                    val.setHp(manager.level);
                    val.setSpeed(manager.level);
                    val.setDelay(manager.level);
                }
            }
            
            //Waveを順次アクティブにする
            if(nowCount == 0)
            {
                print("CreateEnemy");
                foreach (var val in wave)
                {
                    nowCount++;
                    checkActive = 0;
                    nowWave = val;
                    print("nowWave: " + nowWave.name);
                    print("nowWave.childCount: " + nowWave.transform.childCount);
                    val.SetActiveRecursively(true);
                    

                    //Enemyがいなくなったかどうか監視
                    while (nowWave.activeSelf == true)
                    {
                        //Enemyがいなくなった数を監視
                        if (checkActive >= nowWave.transform.childCount)
                        {
                            nowWave.SetActive(false);
                            print(nowWave.name + " = false");
                            nowCount++;
                            print("nowCount: " + nowCount);
                        }
                        else
                        {
                            //Enemyがいる間は待機
                            yield return new WaitForEndOfFrame();
                        }
                    }
                }

            }


            // Waveの子要素のEnemyが全て削除されるまで待機する
            /*while (g.transform.childCount != 0) {
				yield return new WaitForEndOfFrame ();
			}*/

            // Waveの削除
            //Destroy (g);

            // 格納されているWaveを全て実行したらcurrentWaveを0にする（最初から -> ループ）
            //Emitterの子要素であるwaveがすべて非アクティブになったらループ
            /*
			if (waves.Length <= ++currentWave) {
				currentWave = 0;
                manager.level++;
                manager.levelUP();
            }
            */
            if (nowCount >= transform.childCount)
            {
                print("Emitter's Chilren: " + transform.childCount);
                nowCount = 0;
                manager.level++;
                manager.levelUP();
            }

        }
	}   
}