using UnityEngine;
using System.Collections;

public class Manager : MonoBehaviour {

    //Playerプレハブ
    public GameObject player;

    //タイトル
    private GameObject title;

	// Use this for initialization
	void Start () {
        print("TitleStart");
        //Titleゲームオブジェクトを検索し取得する
        title = GameObject.Find("Title");
	
	}

    // Update is called once per frame
    void OnGUI()
    {
        //ゲーム中ではなく、タッチまたはマウスクリック直後であればtrueを返す。
        if(IsPlaying() == false && Event.current.type == EventType.MouseDown)
        {
            GameStart();
        }
    }

    void GameStart()
    {
        //ゲームスタート時に、タイトルを非表示にしてプレイヤーを作成する
        title.SetActive(false);
        print("title: " + title.activeSelf);
        Instantiate(player, player.transform.position, player.transform.rotation);
    }

    public void GameOver()
    {
        //ハイスコアの保存
        FindObjectOfType<Score>().Save();

        print("GameOver");
        //ゲームオーバー時に、タイトルを表示する
        title.SetActive(true);
        print("title: " + title.activeSelf);
    }

    public bool IsPlaying()
    {
        //ゲーム中かどうかはタイトルの表示/非表示で判断する
        return title.activeSelf == false;
    }
}
