using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UniRx.Async;
using UnityEngine;

namespace CompletedAssets {
	public class Ghost : MonoBehaviour {

		Enemy enemy;

		SpriteRenderer mainSpriteRenderer;

		[SerializeField]
		Sprite damegedSprite;

		IEnumerator Start () {
			enemy = GetComponent<Enemy>();
			mainSpriteRenderer = gameObject.GetComponent<SpriteRenderer>();
			while (true) {
				Enemy enemy = gameObject.GetComponent<Enemy> ();
                if (gameObject.GetComponent<Enemy> ().hp <= 0) {
                    OnDeath ();
                }
				// shotDelay秒待つ
				yield return null;
			}
		}

		void OnDeath () {
			UbhShotCtrl[] shotControllers = GetComponents<UbhShotCtrl> ();
            for (int i = 0; i < transform.childCount; i++) {
				transform.GetChild (i).gameObject.SetActive (true);
			}
            foreach (UbhShotCtrl item in shotControllers) {
				item.enabled = false;
                item.StartShotRoutine ();
			}
		}

	}
}