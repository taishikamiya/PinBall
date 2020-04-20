using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallController : MonoBehaviour {

	//ボールが見える可能性のあるz軸の最大値
	private float visiblePosZ = -6.5f;

	//ゲームオーバーを表示するテキスト
	private GameObject gameoverText;

	//点数表示のテキスト
	private int totalScore = 0;
	private GameObject scoreText;

	// Use this for initialization
	void Start () {
		//シーン中のGameOverTextオブジェクトを取得
		this.gameoverText = GameObject.Find("GameOverText");

		//シーン中のScoreTextオブジェクトを取得
		this.scoreText = GameObject.Find("ScoreText");
	}
	
	// Update is called once per frame
	void Update () {
        //ボールが画面外に出た場合
        if(this.transform.position.z < this.visiblePosZ)
        {
			//GameOverTextにゲームオーバー表示
			this.gameoverText.GetComponent<Text>().text = "GameOver";
        }
		
	}

	//衝突時に呼ぶ関数
	private void OnCollisionEnter(Collision other)
	{
		int addScore = 0;
//		Text targetText = this.GetComponent<Text>();
//		int totalScore = int.Parse(targetText);

		//得点計算
		if (other.gameObject.tag == "SmallStarTag")
        {
			addScore += 5;
        } else if(other.gameObject.tag == "LargeStarTag")
        {
			addScore += 20;
        } else if (other.gameObject.tag == "SmallCloudTag")
		{
			addScore += 10;
		} else if (other.gameObject.tag == "LargeCloudTag")
		{
			addScore += 15;
		}

		//ScoreTextに点数加算
		this.totalScore += addScore;

		this.scoreText.GetComponent<Text>().text = totalScore.ToString();
	}
}
