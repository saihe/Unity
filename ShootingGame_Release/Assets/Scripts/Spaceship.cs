using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D), typeof(Animator))]
public class Spaceship : MonoBehaviour
{
	// 移動スピード
	public float speed;
	
	// 弾を撃つ間隔
	public float shotDelay;
	
	// 弾のPrefab
	public GameObject bullet;
	
	// 弾を撃つかどうか
	public bool canShot;
	
	// 爆発のPrefab
	public GameObject explosion;

	// アニメーターコンポーネント
	private Animator animator;


	void Start ()
	{
        //弾を撃つ（コルーチン）
        //StartCoroutine(Shoot());

		// アニメーターコンポーネントを取得
		animator = GetComponent<Animator> ();
	}
    /*
    IEnumerator Shoot()
    {
        while (true)
        {

            // shotDelay秒待つ
            yield return new WaitForSeconds(shotDelay);

            // 子要素を全て取得する
            foreach (Transform child in transform)
            {

                long start = System.DateTime.Now.Ticks;
                // ShotPositionの位置/角度で弾を撃つ
                ObjectPool.instance.GetGameObject(bullet, child.transform.position, child.transform.rotation);

                // 処理時間でInstantiateとObjectPoolを比較してみる
                Debug.Log(System.DateTime.Now.Ticks - start);
            }
        }
    }
    */
    // 爆発の作成
    public void Explosion ()
	{
		Instantiate (explosion, transform.position, transform.rotation);
	}
	
    
	// 弾の作成
	public void Shot (Transform origin)
	{
		Instantiate (bullet, origin.position, origin.rotation);
        try
        {
            //Playerからapを取得
            int ap = FindObjectOfType<Player>().ap;
            //インスタンスしたBulletにapを反映
            Bullet[] bullets = FindObjectsOfType<Bullet>();
            foreach (var val in bullets)
            {
                //配列の中のBulletsすべてにsetPower()
                val.setPower(ap);
            }
        }
        catch
        {

        }
    }
    

	// アニメーターコンポーネントの取得
	public Animator GetAnimator()
	{
		return animator;
	}
}