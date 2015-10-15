using UnityEngine;

public class Manager : MonoBehaviour
{
    // Playerプレハブ
    public GameObject player;
	
    // タイトル
    private GameObject title;

    //ロード中
    private GameObject loading;

    //ステージレベル
    private GameObject stageStart;

    //HP GUIプレハブ
    public GameObject hpGUI;

    //Level up判定
    public int level = 1;
    
    //Level GUI
    public GameObject levelGUI;
    
    // ボタンが押されると対応する変数がtrueになる
    private bool logOutButton;

    //ゲーム開始フラグ
    private bool start;

    void Start ()
    {
        // Titleゲームオブジェクトを検索し取得する
        title = GameObject.Find ("Title");
        loading = GameObject.Find("Loading");
        loading.SetActive(false);
        stageStart = GameObject.Find("Start");
        stageStart.SetActive(false);
        start = false;
    }

	
    void OnGUI() {
        if( start == false){
            drawButton();
            // ログアウトボタンが押されたら
            if( logOutButton)
                {
                    Application.LoadLevel(Application.loadedLevel);
                }
	

            // 画面タップでゲームスタート
            if(loading.activeSelf == false)
            {
                if (Event.current.type == EventType.MouseDown)
                {
                    setLoad();
                }
            }            
        }	
    }

    void setLoad()
    {
        title.SetActive(false);
        loading.SetActive(true);
        start = true;
    }
    public bool getStart()
    {
        return start;
    }
    public bool NowLoading()
    {
        //ロード中かどうか
        return loading.activeSelf == true;
    }
	
    public void GameStart() {
        // ゲームスタート時に、タイトルを非表示にしてプレイヤーを作成する
        loading.SetActive(false);
        Instantiate (player, player.transform.position, player.transform.rotation);
    }
	

    public void GameOver() {

        PlayerPrefs.SetInt ("lastWave", FindObjectOfType<Emitter>().currentWave );
        PlayerPrefs.Save ();

        FindObjectOfType<Score> ().Save ();
        //E/Levelを初期化
        level = 1;
        levelUP();

        // ゲームオーバー時に、タイトルを表示する
        start = false;
    }


    public bool IsPlaying () {
        // ゲーム中 = true
        return start;
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
        levelGUI.GetComponent<GUIText>().text = "E/Level: " + level.ToString();
    }
}