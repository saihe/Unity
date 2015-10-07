using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CommentInputManager : MonoBehaviour {

  public string inputText;

  // ボタンが押されると対応する変数がtrueになる
  private bool submitButton;
  private bool backButton;

  void OnGUI () {
    drawMenu();

    // 送信ボタンが押されたら
    if( submitButton ) {
      // 最後に死んだWaveと、現在のプレイヤー名を取得
      int lastWave  = PlayerPrefs.GetInt ("lastWave", 0);
      string player = FindObjectOfType<UserAuth>().currentPlayer();
      // コメントを作ってサーバーに保存
      Comment comment = new Comment( lastWave, inputText, player );
      comment.save();
      Application.LoadLevel("Stage");
    }

    // 戻るボタンが押されたら
    if( backButton ) {
      Application.LoadLevel("Stage");
    }
  }

  private void drawMenu() {

    // テキストボックスの設置と入力値の取得
    int boxW = 360; int boxH = 50;
    GUI.skin.textField.fontSize = 30;
    int maxLength = 10;
    inputText = GUI.TextField (new Rect(Screen.width*1/2 - boxW/2, Screen.height*1/2 - boxH/2, boxW, boxH), inputText, maxLength);

    // ボタンの設置
    int btnW = 180, btnH = 50;
    GUI.skin.button.fontSize = 20;
    submitButton = GUI.Button( new Rect(Screen.width*1/4 - btnW/2, Screen.height*3/4 - btnH/2, btnW, btnH), "Submit" );
    backButton   = GUI.Button( new Rect(Screen.width*3/4 - btnW/2, Screen.height*3/4 - btnH/2, btnW, btnH), "Back" );

  }
}
