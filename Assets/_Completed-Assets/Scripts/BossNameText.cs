using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace CompletedAssets {
	public class BossNameText : MonoBehaviour {

		Text bossNameText;

		void Start () {
			bossNameText = gameObject.GetComponent<Text> ();
			SubjectProvider.bossBattleStart.Subscribe (bossName => {
				bossNameText.text = bossName;
			}).AddTo (gameObject);
			SubjectProvider.bossBattleEnd.Subscribe (p => {
				bossNameText.text = "";
			}).AddTo (gameObject);
		}

	}
}