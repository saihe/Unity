using UnityEngine;

public class Score : MonoBehaviour
{

  // スコアを表示するGUIText
  public GUIText scoreGUIText;
	
  // ハイスコアを表示するGUIText
  public GUIText highScoreGUIText;
	
  // スコア
  private int score;
	
  // ハイスコア
  private NCMB.HighScore highScore;
	
  // PlayerPrefsで保存するためのキー
  // private string highScoreKey = "highScore";
	
  // ハイスコアが更新されたことを示すフラグ
  private bool isNewRecord;

  void Start ()
  {
    Initialize ();

    // ハイスコアを取得する。保存されてなければ0点。
    string name = FindObjectOfType<UserAuth>().currentPlayer();
    highScore = new NCMB.HighScore( 0, name );
    highScore.fetch();
  }
	
  void Update ()
  {
    // スコアがハイスコアより大きければ
    if (highScore.score < score) {
      isNewRecord = true;
      highScore.score = score;
    }
		
    // スコア・ハイスコアを表示する
    scoreGUIText.text = score.ToString ();
    highScoreGUIText.text = "HighScore : " + highScore.score.ToString ();
  }
	
  // ゲーム開始前の状態に戻す
  private void Initialize ()
  {
    // スコアを0に戻す
    score = 0;
		
    // ハイスコアを取得する。保存されてなければ0を取得する。
    // highScore = PlayerPrefs.GetInt (highScoreKey, 0);
    // Start関数に移す

    // フラグを初期化する
    isNewRecord = false;
  }
	
  // ポイントの追加
  public void AddPoint (int point)
  {
    score = score + point;
  }
	
  // ハイスコアの保存
  public void Save ()
  {
    // ハイスコアを保存する
    // PlayerPrefs.SetInt (highScoreKey, highScore);
    // PlayerPrefs.Save ();
    if( isNewRecord )
      highScore.save();
		
    // ゲーム開始前の状態に戻す
    Initialize ();
  }
}