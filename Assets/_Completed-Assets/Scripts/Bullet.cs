using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace CompletedAssets {
	public class Bullet : MonoBehaviour {

		// ゲームオブジェクト生成から削除するまでの時間
		public float lifeTime = 1;

		// 攻撃力
		public int power = 1;

		private int bulletEnemyLayerNumber = 11;

		private GameObject prefab;

		void Start () {
			// ローカル座標のY軸方向に移動する
			string layerName = LayerMask.LayerToName (gameObject.layer);
			if (layerName == "Bullet (Enemy)") {
				// SubjectProvider.bombSubject.Subscribe (bombStart => {
				// 	if (bombStart) {
				// 		Destroy (gameObject);
				// 	}
				// });
				var disposable = new SingleAssignmentDisposable ();
				disposable.Disposable = SubjectProvider.collisionBullet.Subscribe (p => {
					if (this.gameObject != null) {
						// DestroyMe ();
					}
					disposable.Dispose ();
				});
			}
			if (lifeTime > 0) {
				// lifeTime秒後に削除
				// Destroy (gameObject, lifeTime);
				// Invoke ("DestroyMe", lifeTime);
			}
		}

		void OnTriggerEnter2D (Collider2D c) {
			string layerName = LayerMask.LayerToName (c.gameObject.layer);
			string myLayerName = LayerMask.LayerToName (gameObject.layer);

			if (layerName == "Bullet (Player)" && myLayerName == "Bullet (Enemy)") {
				if (c.gameObject.tag == "Bomb") {
					Store.charismaMiniPool.Place (gameObject.transform.position);
					DestroyMe ();
				}
			} else if (layerName == "Enemy") {
				if (lifeTime > 0) {
					DestroyMe ();
				}
			}

		}

		public void DestroyMe () {
			UbhObjectPool.instance.ReleaseBullet (gameObject.GetComponent<UbhBullet> ());
		}
	}
}