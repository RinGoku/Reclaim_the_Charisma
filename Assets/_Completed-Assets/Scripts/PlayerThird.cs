using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CompletedAssets {
	public class PlayerThird : Player {

		[SerializeField]
		GameObject gungnir;

		override protected IEnumerator UseBomb () {
			SubjectProvider.bombSubject.Value = true;
			GetComponents<AudioSource> () [1].Play ();
			isInvincible = true;
			int margin = 50; //グングニルの間隔
			int gungnirOneSideNum = 7; //片側のグングニルの数
			List<GameObject> gungnirs = new List<GameObject> ();
			// 右方にグングニルを展開
			for (int i = 1; i <= gungnirOneSideNum; i++) {
				Vector3 pos = gameObject.transform.position;
				pos.x = gameObject.transform.position.x + (i * margin);
				GameObject _gungnir = (GameObject) Instantiate (gungnir, pos, gameObject.transform.rotation);
				_gungnir.GetComponent<Gungnir> ().gungnirOrder = (float) i;
				gungnirs.Add (_gungnir);
			}
			// 左方にグングニルを展開
			for (int i = 1; i <= gungnirOneSideNum; i++) {
				Vector3 pos = gameObject.transform.position;
				pos.x = gameObject.transform.position.x - (i * margin);
				GameObject _gungnir = (GameObject) Instantiate (gungnir, pos, gameObject.transform.rotation);
				_gungnir.GetComponent<Gungnir> ().gungnirOrder = (float) i;
				gungnirs.Add (_gungnir);
			}

			yield return new WaitForSeconds (3);
			for (int i = 0; i < gungnirs.Count; i++) {
				Destroy (gungnirs[i]);

			}
			isInvincible = false;
			isBombing = false;
			SubjectProvider.bombSubject.Value = false;
		}
	}
}