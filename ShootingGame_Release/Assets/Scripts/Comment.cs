using NCMB;
using System.Collections.Generic;

public class Comment
{
  public int    wave   { get; private set; }
  public string text   { get; set; }
  public string player { get; private set; }
  
  // コンストラクタ -----------------------------------
  
  public Comment(int _wave, string _text, string _player)
  {
    wave   = _wave;
    text   = _text;
    player = _player;
  }

  // サーバーにコメントを保存 -------------------------

  public void save()
  {
    // Commentクラスのオブジェクトをつくる
    NCMBObject obj = new NCMBObject ("Comment");
	// フィールドを設定して保存
    obj["Wave"]   = wave;
    obj["Text"]   = text;
    obj["Player"] = player;
    obj.SaveAsync ();
  }
  
  // サーバーからコメントをランダムに取得  -----------------

  public void fetchRandomly (int currentWave)
  {
    // データストアの「Comment」クラスから、Waveをキーにして検索
    NCMBQuery<NCMBObject> query = new NCMBQuery<NCMBObject> ("Comment");
    query.WhereEqualTo ("Wave", currentWave);
    query.FindAsync ((List<NCMBObject> commentList, NCMBException e) => {

    	if (e != null) {
	       //取得失敗時の処理
	    } else {
	       //取得成功時の処理
	       if ( commentList.Count != 0 ) {
	         //取得したコメントの数を上限として、乱数を生成
	         System.Random random = new System.Random();
	         int i  = random.Next(commentList.Count);  
	         //ランダムに選んだコメントを保持する
	         wave   = System.Convert.ToInt32 ( commentList[i]["Wave"]   );
	         text   = System.Convert.ToString( commentList[i]["Text"]   );
	         player = System.Convert.ToString( commentList[i]["Player"] );
	       }
	    }
      });
  }

  // コメントを表示するときに使う文字列を整形する
  public string print()
  {
    return text + "\n(by " + player + ")";
  }

}