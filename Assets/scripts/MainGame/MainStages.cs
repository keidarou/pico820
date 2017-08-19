using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainStages : MonoBehaviour {

	//public GameObject CodeVs;
	int stgNum=CreateButton.sendStageNum;

	string[] mapCode={
		"None",//stage0<-これはない
		"1dayo",//stage1
		"2dayooooooooooooo",
		"",
		"",
		"",//stage5
		"",
		"",
		"",
		"",
		"",//stage10
		"",
		"",
		"",
		"",
		"15dayo",//stage15
	};

	// Start前フレームに実行
	void Awake () {
		Debug.Log (stgNum);
		codevisualizer.strcode = mapCode [stgNum];
		Debug.Log ("beat");
		Debug.Log (codevisualizer.strcode);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
