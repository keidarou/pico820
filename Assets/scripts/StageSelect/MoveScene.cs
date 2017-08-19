using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MoveScene : UIBehaviour {
	public GameObject createBtn;

	public int stgNum;//ここにCreateButton.csから番号があたえれられる

	// Use this for initialization
	//protected override void Start () {

		//base.Start ();

		//GetComponent<Button> ().onClick.AddListener (OnClick);
	//}
	public void OnClick(){
		Debug.Log ("de");
		CreateButton.sendStageNum = stgNum;
		SceneManager.LoadScene ("MainGame");

	}

	public void backToTitle(){
		SceneManager.LoadScene ("Title");
	}

}
