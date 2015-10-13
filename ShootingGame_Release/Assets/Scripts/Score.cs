using UnityEngine;

public class Score : MonoBehaviour
{

  // スコアを表示するGUIText
  public GUIText scoreGUIText;
	
  // ハイスコアを表示するGUIText
  public GUIText highScoreGUIText;
	
  // スコア
  private int score;
	
  // PlayerPrefsで保存するためのキー
  // private string highScoreKey = "highScore";
	
  // ハイスコアが更新されたことを示すフラグ
  private bool isNewRecord;

  void Start ()
  {
    Initialize ();
  }
	
  void Update ()
  {
    isNewRecord = true;
		
    // スコア・ハイスコアを表示する
    scoreGUIText.text = score.ToString ();
    highScoreGUIText.text = "HighScore : " + score.ToString ();
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
    // ゲーム開始前の状態に戻す
    Initialize ();
  }
}