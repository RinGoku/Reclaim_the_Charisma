using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UnityEngine;

namespace CompletedAssets {
	public class Enemy : MonoBehaviour {
		// ヒットポイント
		public int hp = 1;

		public int damageThis = 0;

		// スコアのポイント
		public int point = 100;

		// Spaceshipコンポーネント
		Spaceship spaceship;

		public GameObject charismaPoint;

		int charismaPointIndex = -1;

		private IDisposable pauseSub;

		private bool isBoss;

		private UbhShotCtrl[] shotControllers;

		private SpriteRenderer spriteRenderer;

		private bool isVisiblePrev = false;

		void Start () {
			isBoss = GetComponent<Boss> () != null;
			// Spaceshipコンポーネントを取得
			spaceship = GetComponent<Spaceship> ();
			shotControllers = GetComponents<UbhShotCtrl> ();
			spriteRenderer = GetComponent<SpriteRenderer> ();
			foreach (UbhShotCtrl item in shotControllers) {
				item.enabled = false;
			}
			pauseSub = SubjectProvider.pauseEnd.Subscribe (p => {
				foreach (UbhShotCtrl item in shotControllers) {
					item.StartShotRoutine ();
				}
			}).AddTo (gameObject);
		}

		void Update () {
			if (!isVisiblePrev && spriteRenderer.isVisible) {
				foreach (UbhShotCtrl item in shotControllers) {
					item.enabled = true;
					item.StartShotRoutine ();
				}
				isVisiblePrev = true;
			}
		}

		void OnTriggerEnter2D (Collider2D c) {
			if (!spriteRenderer.isVisible) {
				return;
			}
			// レイヤー名を取得
			string layerName = LayerMask.LayerToName (c.gameObject.layer);

			// レイヤー名がBullet (Player)以外の時は何も行わない
			if (layerName != "Bullet (Player)") {
				return;
			}

			// PlayerBulletのTransformを取得
			Transform playerBulletTransform = c.transform.parent;

			Bullet bullet = null;
			// Bulletコンポーネントを取得
			if (playerBulletTransform == null) {
				bullet = c.gameObject.GetComponent<Bullet> ();
			} else {
				bullet = playerBulletTransform.GetComponent<Bullet> ();
			}
			if (bullet == null) {
				bullet = c.transform.GetComponent<Bullet> ();
			}
			if (bullet.transform.position.y > 280) {
				return;
			}
			// ヒットポイントを減らす
			hp -= bullet.power;
			damageThis += bullet.power;
			// 弾の削除
			// ヒットポイントが0以下であれば
			if (hp <= 0) {
				// スコアコンポーネントを取得してポイントを追加
				// FindObjectOfType<Score> ().AddPoint (point);
				// 爆発
				spaceship.Explosion ();
				// 	カリスマポイント放出処理
				Store.charismaPool.Place (gameObject.transform.position);

				if (!isBoss) {
					// エネミーの削除
					StartCoroutine (Utility.DelayMethod (0f, () => {
						Destroy (gameObject);
					}));
				}
				// generatePoint (transform);
			} else {
				// Damageトリガーをセット
				// spaceship.GetAnimator ().SetTrigger ("Damage");

			}
		}
	}
}