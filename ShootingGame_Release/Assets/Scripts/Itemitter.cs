using UnityEngine;
using System.Collections;

public class Itemitter : MonoBehaviour {

    // Waveプレハブを格納する
    public GameObject[] items;

    // 現在のItem
    public int currentItem { get; private set; }

    // Managerコンポーネント
    private Manager manager;

    //Enemyコンポーネント
    private Enemy enemy;

    IEnumerator Start()
    {
       
        // Waveが存在しなければコルーチンを終了する
        if (items.Length == 0)
        {
            yield break;
        }

        // Managerコンポーネントをシーン内から探して取得する
        manager = FindObjectOfType<Manager>();

        while (true)
        {

            // タイトル表示中は待機
            while (manager.IsPlaying() == false)
            {
                yield return new WaitForEndOfFrame();
            }


            // Waveを作成する
            GameObject g = (GameObject)Instantiate(items[currentItem], transform.position, Quaternion.identity);
            // WaveをEmitterの子要素にする
            g.transform.parent = transform;

            // Waveの子要素のEnemyが全て削除されるまで待機する
            while (g.transform.childCount != 0)
            {
                yield return new WaitForEndOfFrame();
            }

            // Waveの削除
            Destroy(g);

            // 格納されているWaveを全て実行したらcurrentWaveを0にする（最初から -> ループ）
            if (items.Length <= ++currentItem)
            {
                currentItem = 0;
            }
        }
    }
}
