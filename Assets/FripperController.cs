using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FripperController : MonoBehaviour {

	//HingeJointコンポーネントを入れる
	private HingeJoint myHingeJoint;

	//初期の傾き
	private float defaultAngle = 20;
	//弾いたときの傾き
	private float flickAngle = -20;

	// Use this for initialization
	void Start () {
		//HingeJointコンポーネント取得
		this.myHingeJoint = GetComponent<HingeJoint>();

		//フリッパーの傾きを設定
		SetAngle(this.defaultAngle);
	}
	
	// Update is called once per frame
	void Update () {
        //左矢印キーを押したとき左フリッパー動かす
        if(Input.GetKeyDown(KeyCode.LeftArrow)&&tag == "LeftFripperTag")
        {
			SetAngle(this.flickAngle);
        }
        //右矢印キーを押したとき右フリッパーを動かす
        if (Input.GetKeyDown(KeyCode.RightArrow) && tag == "RightFripperTag")
        {
			SetAngle(this.flickAngle);
        }

        //矢印キーが離された時フリッパーを戻す
        if (Input.GetKeyUp(KeyCode.LeftArrow) && tag == "LeftFripperTag")
        {
			SetAngle(this.defaultAngle);
        }
        if (Input.GetKeyUp(KeyCode.RightArrow) && tag == "RightFripperTag")
        {
			SetAngle(this.defaultAngle);
        }

        //Touch
        if(Input.touchCount > 0)
        {
            foreach(Touch t in Input.touches)
            {
                //左半分を触った時
                if(t.phase == TouchPhase.Began && t.position.x<Screen.width/2 && tag == "LeftFripperTag")
                {
                    SetAngle(this.flickAngle);
                }
                //右半分を触った時
                if (t.phase == TouchPhase.Began && t.position.x >= Screen.width / 2 && tag == "RightFripperTag")
                {
                    SetAngle(this.flickAngle);
                }
                //離れた時
                if (t.phase == TouchPhase.Ended && tag == "LeftFripperTag")
                {
                    SetAngle(this.defaultAngle);
                }
                if (t.phase == TouchPhase.Ended && tag == "RightFripperTag")
                {
                    SetAngle(this.defaultAngle);
                }

            }
        }

	}

    //フリッパーの傾き設定
    public void SetAngle(float angle)
    {
		JointSpring jointSpr = this.myHingeJoint.spring;
		jointSpr.targetPosition = angle;
		this.myHingeJoint.spring = jointSpr;
    }
}
