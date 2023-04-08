using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UniRx;
using UniRx.Async;
using UnityEngine;

namespace CompletedAssets {
	public class Boss : MonoBehaviour {
		public string bossName;

		[SerializeField]
		public BarrangeInfo[] barranges;

		public string standName;

		public int[] hpBorders;

		private GameObject stand;

		Spaceship spaceship;

		[SerializeField]
		private GameObject hpGagePrefab;

		private GameObject _hpGagePrefab;

		private HpGage hpGage;

		float initialA = 1f;

		private int maxHp;
		[SerializeField]
		private int charismaPointNum;

		[SerializeField]
		private int charismaPointRange;

		private GenericPool charismaMiniPool;

		private GenericPool charismaPool;
		[SerializeField]
		private GameObject cutIn;

		private GameObject targetCutInObj;

		IEnumerator Start () {
			// Spaceshipコンポーネントを取得
			spaceship = GetComponent<Spaceship> ();
			// 最大HPの取得
			maxHp = gameObject.GetComponent<Enemy> ().hp;

			charismaMiniPool = Store.charismaMiniPool;
			charismaPool = Store.charismaPool;

			// HPゲージ初期化
			_hpGagePrefab = Instantiate (hpGagePrefab, gameObject.transform.position, gameObject.transform.rotation);
			_hpGagePrefab.transform.parent = gameObject.transform;
			_hpGagePrefab.GetComponent<UIController> ().SetTargetTfm (gameObject.transform);
			_hpGagePrefab.SetActive (true);
			hpGage = GameObject.Find ("Gage").GetComponent<HpGage> ();
			hpGage.SetName (bossName);
			// ローカル座標のY軸のマイナス方向に移動する
			// Move (transform.up * -1);
			SpriteRenderer rend = null;
			Color spriteColor = new Color (0, 0, 0, 0);
			Boss bossInfo = gameObject.GetComponent<Boss> ();
			BarrangeInfo[] barranges = null;
			if (bossInfo != null) {
				barranges = bossInfo.barranges;
			}
			BarrangeInfo prevShot = null;
			int prevShotIndex = 0;
			SubjectProvider.bossBattleStart.OnNext (bossInfo.bossName);
			while (true) {
				Enemy enemy = gameObject.GetComponent<Enemy> ();
				int damageThis = enemy.damageThis;
				int nowHp = enemy.hp;
				hpGage.UpdateHp (nowHp, maxHp);
				if (spaceship.canShot && barranges != null && barranges.Length > 0) {
					int nowBorderIndex = 0;
					for (int j = 0; j < hpBorders.Length; j++) {
						if (damageThis >= hpBorders[j]) {
							nowBorderIndex = j;
						}
					}
					// ボス戦の場合、スペルカード切り替わり時にエフェクトを入れる
					BarrangeInfo nowShot = barranges[nowBorderIndex];
					if (prevShot == null || !prevShot.Equals (nowShot)) {
						SwitchOffBarrage (nowShot);
						SubjectProvider.barrangeSwitched.OnNext (nowShot);

						// 一番最初の弾幕ではカットインは出さず、弾幕も消さない
						if (prevShot != null) {
							var disposable = new SingleAssignmentDisposable ();
							BulletToPoint ();
							GetComponent<AudioSource> ().Play ();
							// カットイン表示
							targetCutInObj = Instantiate (cutIn, cutIn.transform.position, cutIn.transform.rotation);
							StartCoroutine (Utility.DelayMethod (1.5f, () => {
								Destroy (targetCutInObj);
								// 現弾幕発射処理を無効に
								SwitchOnBarrage (nowShot);
							}));
						} else {
							SwitchOnBarrage (nowShot);
						}
					}
					prevShot = nowShot;
					if (gameObject.GetComponent<Enemy> ().hp <= 0) {
						OnDeath ();
					}
				}

				// shotDelay秒待つ
				yield return new WaitForSeconds (spaceship.shotDelay);
			}
		}

		void SwitchOnBarrage (BarrangeInfo nowShot) {
			for (int i = 0; i < transform.childCount; i++) {
				// 敵弾発射はUni Bullet Hellによって作成するため該当箇所はコメントアウト
				if (nowShot.shotNames.Contains (transform.GetChild (i).name)) {
					transform.GetChild (i).gameObject.SetActive (true);
					// prevShotIndex = i;
				}
			}
		}

		void SwitchOffBarrage (BarrangeInfo nowShot) {
			for (int i = 0; i < transform.childCount; i++) {
				// 敵弾発射はUni Bullet Hellによって作成するため該当箇所はコメントアウト
				if (!nowShot.shotNames.Contains (transform.GetChild (i).name)) {
					if (transform.GetChild (i).gameObject.name != "HPGage(Clone)") {
						transform.GetChild (i).gameObject.SetActive (false);
					}
				}
			}
		}

		void BulletToPoint () {
			GameObject[] bullets = GameObject.FindGameObjectsWithTag ("Bullet");
			// 今の弾幕をカリスマポイントに変える
			for (int i = 0; i < bullets.Length; i++) {
				// 	カリスマポイント放出処理
				GameObject bullet = bullets[i];
				// posPoint (bullet.transform.position);
				UbhObjectPool.instance.ReleaseBullet (bullet.GetComponent<UbhBullet> ());
			}
			// Destroy (bullet);
		}

		void posPoint (Vector2 pos) {
			charismaMiniPool.Place (pos);
			// yield return null;
		}

		void OnDeath () {
			BulletToPoint ();
			SubjectProvider.bossBattleEnd.OnNext ("");
			Vector3 pos = gameObject.transform.position;
			for (int i = 0; i < charismaPointNum; i++) {
				float x = Random.Range (pos.x - charismaPointRange, pos.x + charismaPointRange);
				float y = Random.Range (pos.y - charismaPointRange, pos.y + charismaPointRange);
				charismaPool.Place (new Vector2 (x, y));
			}
			Destroy (_hpGagePrefab);
			Destroy (gameObject);
			if (targetCutInObj != null) {
				Destroy(targetCutInObj);
			}
		}

		IEnumerator FadeOutSprite (GameObject obj) {
			SpriteRenderer rend = obj.GetComponent<SpriteRenderer> ();
			Color spriteColor = rend.color;
			// フェードアウト
			while (spriteColor.a >= 0) {
				spriteColor.a -= Time.deltaTime / 2;
				rend.color = new Color (spriteColor.r, spriteColor.g, spriteColor.b, spriteColor.a);
				Vector3 pos = obj.transform.position;
				pos.y += 1;
				obj.transform.position = pos;
				yield return null;
			}
		}

	}
}