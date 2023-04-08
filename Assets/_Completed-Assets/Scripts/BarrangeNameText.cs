using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace CompletedAssets {
	public class BarrangeNameText : MonoBehaviour {

		Text barrangeNameText;

		void Start () {
			barrangeNameText = gameObject.GetComponent<Text> ();
			SubjectProvider.barrangeSwitched.Subscribe (barrenge => {
				barrangeNameText.text = barrenge.barrangeName;
			}).AddTo (gameObject);
			SubjectProvider.bossBattleEnd.Subscribe (p => {
				barrangeNameText.text = "";
			}).AddTo (gameObject);
		}

	}
}