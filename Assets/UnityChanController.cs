using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnityChanController : MonoBehaviour
{

    //アニメーションするためのコンポーネントを入れる
    private Animator myAnimator;

    //Unityちゃんを移動させるためのコンポーネントを入れる(追加)
    private Rigidbody myRigidbody;

    //前進するための力(追加)
    private float forwardForce = 800.0f;

    //左右に移動するための力(追加)
    private float turnForce = 500.0f;

    //ジャンプするための力(追加)
    private float upForce = 500.0f;

    //左右の移動できる範囲(追加)
    private float movableRange = 3.4f;

    //衝突したら速度を減速させる係数
    private float coefficient = 0.95f;

    //ゲーム終了の判定
    private bool isEnd = false;

    //ゲーム終了時に表示するテキスト
    private GameObject stateText;

    //ポイントを表示するテキスト
    private GameObject ScoreText;

    //得点
    private int score = 0;

    //左ボタン押下の判定
    private bool isLButtonDown;

    //右ボタン押下の判定
    private bool isRButtonDown;

    // Use this for initialization  
    void Start()
    {
        //Animationコンポーネントを取得
        this.myAnimator = GetComponent<Animator>();

        //走るアニメーションを開始
        this.myAnimator.SetFloat("Speed", 1);

        //Rigidbodyコンポーネントを取得
        this.myRigidbody = GetComponent<Rigidbody>();

        //シーン中のテキストオブジェクトを取得
        this.stateText = GameObject.Find("GameResultText");
        this.ScoreText = GameObject.Find("ScoreText");
    }

    // Update is called once per frame
    void Update()
    {
        //ゲーム終了ならUnityちゃんの動きを軽減させる
        if (this.isEnd)
        {
            this.forwardForce *= this.coefficient;
            this.turnForce *= this.coefficient;
            this.upForce *= this.coefficient;
            this.myAnimator.speed *= this.coefficient;

        }

        //Unityちゃんに前方向の力を加える(追加)
        this.myRigidbody.AddForce(this.transform.forward * this.forwardForce);

        //Unityちゃんを矢印キーまたはボタンに応じて左右に移動させる(追加)
        if ((Input.GetKey(KeyCode.LeftArrow) || this.isLButtonDown) && -movableRange < this.transform.position.x)
        {
            //左に移動(追加)
            this.myRigidbody.AddForce(-this.turnForce, 0, 0);
        }
        else if ((Input.GetKey(KeyCode.RightArrow) || this.isRButtonDown) && movableRange > this.transform.position.x)
        {
            //右に移動(追加)
            this.myRigidbody.AddForce(this.turnForce, 0, 0);
        }

        //Jumpステートの場合はJumpにfalseをセットする
        if (this.myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Jump"))
        {
            this.myAnimator.SetBool("Jump", false);
        }

        //ジャンプしていない時にスペースが押されたらジャンプする(追加)
        if (Input.GetKeyDown(KeyCode.Space) && this.transform.position.y < 0.5f)
        {
            //ジャンプアニメを再生(追加)
            this.myAnimator.SetBool("Jump", true);
            //Unityちゃんに上方向の力を加える(追加)
            this.myRigidbody.AddForce(this.transform.up * this.upForce);
        }
    }
    //トリガーモードで他のオブジェクトと接触した場合の処理
    void OnTriggerEnter(Collider other){
        //障害物に衝突した場合
        if(other.gameObject.tag == "CarTag" || other.gameObject.tag == "TrafficConeTag"){
            this.isEnd = true;
            //stateTextにGAME OVERを表示
            this.stateText.GetComponent<Text>().text = "GAME OVER";
        }
        //ゴールした場合の処理
        if(other.gameObject.tag == "GoalTag"){
            this.isEnd = true;
            //stateTextにGAME CLEARを表示
            this.stateText.GetComponent<Text>().text = "GAME CLEAR!!";
        }
        //コインに衝突した場合
        if(other.gameObject.tag == "CoinTag"){
            //得点を追加
            score += 10;

            this.ScoreText.GetComponent<Text>().text = "Score " + this.score + "pt";

            GetComponent<ParticleSystem>().Play();

            //接触したコインを破棄
            Destroy(other.gameObject);
        }
    }

    //ジャンプボタンを押した場合の処理（追加）
	public void GetMyJumpButtonDown()
	{
		if (this.transform.position.y < 0.5f)
		{
			this.myAnimator.SetBool("Jump", true);
			this.myRigidbody.AddForce(this.transform.up * this.upForce);
		}
	}

	//左ボタンを押し続けた場合の処理（追加）
	public void GetMyLeftButtonDown()
	{
		this.isLButtonDown = true;
	}
	//左ボタンを離した場合の処理（追加）
	public void GetMyLeftButtonUp()
	{
		this.isLButtonDown = false;
	}

	//右ボタンを押し続けた場合の処理（追加）
	public void GetMyRightButtonDown()
	{
		this.isRButtonDown = true;
	}
	//右ボタンを離した場合の処理（追加）
	public void GetMyRightButtonUp()
	{
		this.isRButtonDown = false;
	}
}

