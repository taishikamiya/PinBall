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

    // Fripper Flag
    private int leftFripperTouchId = -1;
    private int rightFripperTouchId = -1;


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
//        if(Input.touchCount > 0)  //input.touchesで判定してるのでなくてもOK
//        {

            foreach (Touch t in Input.touches)
            {

                //左半分を触った時
                if(t.phase == TouchPhase.Began && t.position.x<Screen.width/2 && tag == "LeftFripperTag")
                {
                    SetAngle(this.flickAngle);
                    this.leftFripperTouchId = t.fingerId;
                    Debug.Log("L:"+leftFripperTouchId);
                }
                //右半分を触った時
                if (t.phase == TouchPhase.Began && t.position.x >= Screen.width / 2 && tag == "RightFripperTag")
                {
                    SetAngle(this.flickAngle);
                    this.rightFripperTouchId = t.fingerId;
                    Debug.Log("R:"+rightFripperTouchId);
                }


                //左を離した時 エリア判定追加20200421
                //エリア内処理
                if (t.phase == TouchPhase.Ended && t.fingerId==this.leftFripperTouchId && tag == "LeftFripperTag")
                {
                    Debug.Log("Release L:" + t.fingerId);
                    SetAngle(this.defaultAngle);
                    this.leftFripperTouchId = -1;
                }


                //右を離した時 エリア判定追加20200421
                //エリア内処理

                if (t.phase == TouchPhase.Ended && t.fingerId==this.rightFripperTouchId && tag == "RightFripperTag")
                {
                    Debug.Log("Release R:" + t.fingerId);
                    SetAngle(this.defaultAngle);
                    this.rightFripperTouchId = -1;

                }


            }
       // }

	}

    //フリッパーの傾き設定
    public void SetAngle(float angle)
    {
		JointSpring jointSpr = this.myHingeJoint.spring;
		jointSpr.targetPosition = angle;
		this.myHingeJoint.spring = jointSpr;
    }
}
