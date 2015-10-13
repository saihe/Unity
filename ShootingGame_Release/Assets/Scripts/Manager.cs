using UnityEngine;

public class Manager : MonoBehaviour
{
    // Playerプレハブ
    public GameObject player;
	
    // タイトル
    private GameObject title;

    //HP GUIプレハブ
    public GameObject hpGUI;

    //Level up判定
    public int level = 1;
    
    //Level GUI
    public GameObject levelGUI;
    
    // ボタンが押されると対応する変数がtrueになる
    private bool logOutButton;

    void Start ()
    {
        // Titleゲームオブジェクトを検索し取得する
        title = GameObject.Find ("Title");
    }

	
  void OnGUI() {
    if( !IsPlaying() ){
      drawButton();
      // ログアウトボタンが押されたら
      if( logOutButton)
            {
                Application.LoadLevel(Application.loadedLevel);
            }
	

      // 画面タップでゲームスタート
      if ( Event.current.type == EventType.MouseDown) 
	GameStart ();
    }	
  }

	
  void GameStart() {
    // ゲームスタート時に、タイトルを非表示にしてプレイヤーを作成する
    title.SetActive (false);
    Instantiate (player, player.transform.position, player.transform.rotation);
    //HPを左下に表示
    Instantiate(hpGUI, hpGUI.transform.position, hpGUI.transform.rotation);
  }
	

  public void GameOver() {

    PlayerPrefs.SetInt ("lastWave", FindObjectOfType<Emitter>().currentWave );
    PlayerPrefs.Save ();

    FindObjectOfType<Score> ().Save ();
    // ゲームオーバー時に、タイトルを表示する
    title.SetActive (true);
  }


  public bool IsPlaying () {
    // ゲーム中かどうかはタイトルの表示/非表示で判断する
    return title.activeSelf == false;
  }


  private void drawButton() {    
    // ボタンの設置
    int btnW = 120, btnH = 50;
    GUI.skin.button.fontSize = 18;
    logOutButton      = GUI.Button( new Rect(2*btnW, 0, btnW, btnH), "Log Out" );
  }

    public void levelUP()
    {
        print("Level: " + level);
        levelGUI.GetComponent<GUIText>().text = "Level: " + level.ToString();
    }
}