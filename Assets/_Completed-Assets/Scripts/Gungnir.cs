using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;

namespace CompletedAssets {
	public class Gungnir : MonoBehaviour {

		float angle = 120.0f;
		Transform target;

		[SerializeField]
		GameObject targetObj;

		public float gungnirOrder;

		void Start () {
			FadeIn ();
			iTween.MoveTo (gameObject, iTween.Hash ("y", gameObject.transform.position.x + 1000, "time", 2, "delay", gungnirOrder / 5f));
		}

		void FadeIn () {
			// SetValue()を毎フレーム呼び出して、１秒間に０から１までの値の中間値を渡す
			iTween.ValueTo (gameObject, iTween.Hash ("from", 0f, "to", 1f, "time", 0.5f, "onupdate", "SetValue"));
		}

		void SetValue (float alpha) {
			// iTweenで呼ばれたら、受け取った値をImageのアルファ値にセット
			Color thisColor = gameObject.GetComponent<SpriteRenderer> ().color;
			gameObject.GetComponent<SpriteRenderer> ().color = new Color (255, 255, 255, alpha);
		}

	}
}