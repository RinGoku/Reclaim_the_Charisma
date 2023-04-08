using System;
using System.Collections;
using System.Linq;
using UnityEngine;

namespace CompletedAssets {
	public class Player : MonoBehaviour {
		// Spaceshipコンポーネント
		Spaceship spaceship;

		// 移動速度
		public float speedNormal; //通常速
		public float speedLow; //低速

		private bool enabled = true; //操作可能かどうか
		[SerializeField]
		private string[] normalShots;

		SpriteRenderer MainSpriteRenderer;

		private PlayerState NowState;

		public string standName;

		protected bool isInvincible = false;

		public float fadeSpeed = 0.001f;

		public GameObject stand;

		float initialA = 0.5f;

		private int bombNum;

		private int playerNum;

		private PlayerManager _parent;

		protected bool isBombing = false;

		PlayerManager pm;

		IEnumerator Start () {
			_parent = transform.root.gameObject.GetComponent<PlayerManager> ();
			// Spaceshipコンポーネントを取得
			spaceship = GetComponent<Spaceship> ();
			MainSpriteRenderer = gameObject.GetComponent<SpriteRenderer> ();
			speedNormal = 700;
			speedLow = 190;
			NowState = PlayerState.First;
			// 当たり判定を非表示に
			HideHitArea ();
			UbhShotCtrl[] ctrls = GetComponents<UbhShotCtrl> ();
			pm = GameObject.FindGameObjectWithTag ("PlayerManager").GetComponent<PlayerManager> ();
			while (true) {
				bool canBomb = pm.canBomb;
				// ボム使用時
				if (!isBombing && Input.GetKeyDown (KeyCode.X) && enabled && _parent.getBombNum () > 0 && canBomb) {
					isInvincible = true;
					isBombing = true;
					StartCoroutine (UseBomb ());
				}
				//　通常弾発射時
				if (Input.GetKey (KeyCode.Z) && enabled) {
					StartCoroutine (ShotNormal ());
				} else {
					for (int i = 0; i < transform.childCount; i++) {
						if (normalShots.Contains (transform.GetChild (i).name)) {
							transform.GetChild (i).gameObject.SetActive (false);
						}
					}
					// ショット音を消す
					// GetComponents<AudioSource> () [0].loop = false;
					// GetComponents<AudioSource> () [0].Stop ();
				}

				yield return null;
			}
		}

		protected virtual IEnumerator ShotNormal () {
			for (int i = 0; i < transform.childCount; i++) {
				if (normalShots.Contains (transform.GetChild (i).name)) {
					GameObject targetShot = transform.GetChild (i).gameObject;
					targetShot.SetActive (true);
				}
			}
			// ショット音を鳴らす
			// GetComponents<AudioSource> () [0].loop = true;
			// GetComponents<AudioSource> () [0].Play ();
			// shotDelay秒待つ
			yield return new WaitForSeconds (spaceship.shotDelay);
		}

		protected virtual IEnumerator UseBomb () {
			SubjectProvider.bombSubject.Value = true;
			GetComponents<AudioSource> () [1].Play ();
			transform.GetChild (0).gameObject.SetActive (true);
			// float initialY = stand.transform.position.y;
			// rend.color = new Color (spriteColor.r, spriteColor.g, spriteColor.b, initialA);
			// StartCoroutine (FadeOutSprite (stand));
			yield return new WaitForSeconds (2);
			transform.GetChild (0).gameObject.SetActive (false);
			isInvincible = false;
			// rend.color = new Color (spriteColor.r, spriteColor.g, spriteColor.b, 0);
			// Vector3 poss = stand.transform.position;
			// poss.y = initialY;
			// stand.transform.position = poss;
			isBombing = false;
			SubjectProvider.bombSubject.Value  = false;
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

		IEnumerator FadeInSprite (SpriteRenderer rend) {
			Color spriteColor = rend.color;

			// フェードイン
			while (spriteColor.a <= 1) {
				spriteColor.a += Time.deltaTime * fadeSpeed;
				rend.color = new Color (spriteColor.r, spriteColor.g, spriteColor.b, spriteColor.a);
				yield return null;
			}
		}

		void Update () {
			if (enabled) {
				// 右・左
				float x = Input.GetAxisRaw ("Horizontal");

				// 上・下
				float y = Input.GetAxisRaw ("Vertical");

				// 移動する向きを求める
				Vector2 direction = new Vector2 (x, y).normalized;

				// 移動の制限
				Move (direction);
			}
		}

		// 機体の移動
		void Move (Vector2 direction) {
			// 画面左下のワールド座標をビューポートから取得
			Vector2 min = Camera.main.ViewportToWorldPoint (new Vector2 (0, 0));

			// 画面右上のワールド座標をビューポートから取得
			Vector2 max = Camera.main.ViewportToWorldPoint (new Vector2 (1, 1));

			// プレイヤーの座標を取得
			Vector2 pos = transform.position;

			// 移動量を加える
			bool isShift = Input.GetKey (KeyCode.LeftShift) || Input.GetKey (KeyCode.RightShift);
			if (isShift) {
				pos += direction * speedLow * Time.deltaTime;
				// 当たり判定を非表示に
				VisibleHitArea ();
			} else {
				pos += direction * speedNormal * Time.deltaTime;
				// 当たり判定を非表示に
				HideHitArea ();
			}

			// プレイヤーの位置が画面内に収まるように制限をかける
			pos.x = Mathf.Clamp (pos.x, min.x + 32, max.x - (250 + 32));
			pos.y = Mathf.Clamp (pos.y, min.y + 32, max.y - 32);

			// 制限をかけた値をプレイヤーの位置とする
			transform.position = pos;
		}

		// ぶつかった瞬間に呼び出される
		void OnTriggerEnter2D (Collider2D c) {
			if (!enabled) {
				return;
			}
			// レイヤー名を取得
			string layerName = LayerMask.LayerToName (c.gameObject.layer);

			// レイヤー名がBullet (Enemy)の時は弾を削除
			if (layerName == "Bullet (Enemy)") {
				// 弾の削除
				Destroy (c.gameObject);
			}

			DebugMode debugMode = pm.GetComponent<DebugMode>();
			bool isInvincibleMode = debugMode != null && debugMode.invincibleMode;
			if (isInvincibleMode) {
				return;
			}
			// レイヤー名がBullet (Enemy)またはEnemyの場合は爆発
			if (!isInvincible && layerName == "Bullet (Enemy)" || layerName == "Enemy") {
				HideHitArea ();
				// 爆発する
				spaceship.Explosion ();
				// プレイヤー管理側に死亡を通知
				SubjectProvider.deathSubject.OnNext (-1);
				makeDisabled ();
				SpriteRenderer rend = gameObject.GetComponent<SpriteRenderer> ();
				Color spriteColor = rend.color;
				rend.color = new Color (spriteColor.r, spriteColor.g, spriteColor.b, 0);
				StartCoroutine (Utility.DelayMethod (2f, () => {
					makeEnabled ();
					rend.color = new Color (spriteColor.r, spriteColor.g, spriteColor.b, 100);
					VisibleHitArea ();
					spaceship.GetAnimator ().SetTrigger ("Invincible");
				}));
			}

		}

		public void makeDisabled () {
			enabled = false;
		}

		public void makeEnabled () {
			enabled = true;
		}

		public PlayerState GetPlayerState () {
			return NowState;
		}

		public void SetPlayerState (PlayerState state) {
			NowState = state;
		}

		private void HideHitArea () {
			Utility.toVisibilityHidden (transform.GetChild (1).gameObject);
		}

		private void VisibleHitArea () {
			Utility.toVisible (transform.GetChild (1).gameObject);
		}
	}
}