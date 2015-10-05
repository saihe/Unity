using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class Spaceship : MonoBehaviour {

    //移動スピード
    public float speed;

    //弾を撃つ間隔
    public float shotDelay;

    //弾のPrefab
    public GameObject bullet;

    //弾を撃つかどうか
    public bool canShot;

    //爆発のPrefab
    public GameObject explosion;

    //アニメーターコンポーネント
    private Animator animator;

    // Use this for initialization
    void Start()
    {
        //アニメーターコンポーネントを取得
        animator = GetComponent<Animator>();
    }
    //爆発の生成
    public void Explosion()
    {
        Instantiate(explosion, transform.position, transform.rotation);
    }
    
    //弾の作成
    public void Shot (Transform origin)
    {
        Instantiate(bullet, origin.position, origin.rotation);
    }   

    //アニメーターコンポーネントの取得
    public Animator GetAnimator()
    {
        return animator;
    }

    //機体の移動
    public void Move (Vector2 direction)
    {
        print(direction);
        GetComponent<Rigidbody2D>().velocity = direction * speed;
    }

	
	// Update is called once per frame
	void Update () {
	
	}
}
