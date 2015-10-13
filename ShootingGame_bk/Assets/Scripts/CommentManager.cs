using UnityEngine;
using System.Collections;

public class CommentManager : MonoBehaviour {

  private int     oldWave;
  private Comment comment;
  private GameObject commentGUI;
  
  void Start () {
    oldWave = -1;
    comment = new Comment(0, null, null);
    commentGUI = GameObject.Find ("CommentGUI");
  }
  
  void Update () {
    // waveの切り替わりを監視し、切り替わったらコメントをフェッチ
    int currentWave = FindObjectOfType<Emitter>().currentWave;
    if( oldWave != currentWave)
      {
	oldWave = currentWave;
	comment.fetchRandomly(currentWave);
      }

    // コメントのフェッチが終わったら画面に表示
    // フェッチの完了は、textフィールドが埋まったかどうかで判定
    if( comment.text != null && FindObjectOfType<Manager>().IsPlaying() )
      {
	commentGUI.GetComponent<GUIText>().text = comment.print();
	commentGUI.GetComponent<Animation>().Play();
	// 表示が終わったらまたnullに戻す
	comment.text = null;
      }
  }
}
