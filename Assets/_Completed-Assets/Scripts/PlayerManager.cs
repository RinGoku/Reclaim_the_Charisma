using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UniRx.Async;
using UnityEngine;
using System.Threading.Tasks;

namespace CompletedAssets {
	public class PlayerManager : MonoBehaviour {
		// Spaceshipコンポーネント
		Spaceship spaceship;

		// 移動速度
		private int charismaPoint = 0;

		public GameObject[] players;

		public int[] charismaBorders;

		public int nowPlayerIndex;

		public bool canBomb = true;

		private int bombNum;

		private int playerNum;

		[SerializeField]
		private int initPlayerNum = 3;

		[SerializeField]
		private int initBombNum = 3;

		[SerializeField]
		private GameObject levelUpLayerPrefab;

		private GameObject levelUpLayer;

		private GameObject nowPlayer;

		private Score score;

		private Manager manager;

		[SerializeField]
		private GameObject pointUpSourcePrefab;

		private AudioSource pointUpSource;

		[SerializeField]
		private GameObject levelUpSourcePrefab;

		private AudioSource levelUpSource;

		[SerializeField]
		private GameObject[] cutIns;

		private GameObject targetCutInObj;

		[SerializeField]
		private GameObject[] levelUpCutIns;

		private GameObject targetLevelUpCutInObj;

		private DebugMode debugMode;

		private int bombGetNum = 0;

		private int playerGetNum = 0;

		void Start () {
			bombNum = initBombNum;
			playerNum = initPlayerNum;
			manager = FindObjectOfType<Manager> ();
			score = GameObject.FindGameObjectWithTag ("Score").GetComponent<Score> ();
			for (int i = 1; i <= playerNum; i++) {
				GameObject.Find ("playerPoints").transform.Find ("playerPoint" + i).gameObject.SetActive (true);
			}
			for (int i = 1; i <= bombNum; i++) {
				GameObject.Find ("bombPoints").transform.Find ("bombPoint" + i).gameObject.SetActive (true);
			}
			nowPlayerIndex = 0;
			nowPlayer = (GameObject) Instantiate (players[nowPlayerIndex], players[nowPlayerIndex].transform.position, players[nowPlayerIndex].transform.rotation);
			nowPlayer.transform.parent = gameObject.transform;
			pointUpSource = Instantiate (pointUpSourcePrefab, pointUpSourcePrefab.transform.position, pointUpSourcePrefab.transform.rotation).GetComponent<AudioSource> ();
			levelUpSource = Instantiate (levelUpSourcePrefab, levelUpSourcePrefab.transform.position, levelUpSourcePrefab.transform.rotation).GetComponent<AudioSource> ();

			// カリスマポイント取得通知を購読
			subscribeCharismaPoint ();
			// 死亡通知を購読
			subscribeDeath ();
			// ボム使用通知を購読
			subscribeBombUse ();

			debugMode = GetComponent<DebugMode>();
		}

		private void subscribeCharismaPoint () {
			SubjectProvider.charismaPointSubject.Subscribe (async point => {
				charismaPoint += point;
				score.AddPoint (point);
				if (charismaPoint > 20000 && charismaPoint > (20000 * bombGetNum + 1)) {
					bombGetNum++;
					if (bombNum < 5) {
						bombNum++;
						GameObject.Find ("bombPoints").transform.Find ("bombPoint" + bombNum).gameObject.SetActive (true);
						pointUpSource.Play ();
					}
				}
				if (charismaPoint > 30000 && charismaPoint > (30000 * playerGetNum + 1)) {
					playerGetNum++;
					if (playerNum < 5) {
						playerNum++;
						GameObject.Find ("playerPoints").transform.Find ("playerPoint" + playerNum).gameObject.SetActive (true);
						pointUpSource.Play ();
					}
				}
				// 最終形態はカリスマボーダーをチェックする必要がない
				if (nowPlayerIndex < charismaBorders.Length && charismaPoint >= charismaBorders[nowPlayerIndex]) {
                    GameObject nextPlayer = players[nowPlayerIndex + 1];
                    nowPlayerIndex++;
                    await UniTask.WaitUntil( () => {
                        return !CheckBombExists();
                    });
					if (levelUpSource != null) {
						levelUpSource.Play ();
					}
					if (nowPlayer != null) {
						GameObject _nextPlayer = (GameObject) Instantiate (nextPlayer, nowPlayer.transform.position, nowPlayer.transform.rotation);
						Destroy (nowPlayer);
						nowPlayer = _nextPlayer;
						nowPlayer.transform.parent = gameObject.transform;
						// levelUpLayer = GameObject.Instantiate (levelUpCutIns) as GameObject;
						GameObject targetCutIn = levelUpCutIns[nowPlayerIndex - 1];
						targetLevelUpCutInObj = Instantiate (targetCutIn, targetCutIn.transform.position, targetCutIn.transform.rotation);
						StartCoroutine (Utility.DelayMethod (1.5f, EraseLevelUpCutIn));
					}

				}
			}).AddTo (gameObject);
		}

        private bool CheckBombExists()
        {
            return SubjectProvider.bombSubject.Value;
        }

		public void refreshNowPlayer (Transform trans) {
			GameObject _nextPlayer = (GameObject) Instantiate (nowPlayer, trans.position, trans.rotation);
			Destroy (nowPlayer);
			nowPlayer = _nextPlayer;
			nowPlayer.transform.parent = gameObject.transform;
		}

		private void subscribeDeath () {
			SubjectProvider.deathSubject.Subscribe (point => {
				if (GameObject.Find ("playerPoint" + playerNum.ToString ())) {
					GameObject.Find ("playerPoint" + playerNum.ToString ()).SetActive (false);
				}
				playerNum += point;
				if (bombNum < 3) {
					bombNum = 3;
					for (int i = 1; i <= bombNum; i++) {
						GameObject.Find ("bombPoints").transform.Find ("bombPoint" + i).gameObject.SetActive (true);
					}
				}
				if (playerNum == 0) {
					// プレイヤー状態を初期化
					nowPlayerIndex = 0;
					// ManagerコンポーネントのGameOverメソッドを呼び出す
					manager.GameOver ();
					// プレイヤーを削除
					Destroy (gameObject);
				}
			}).AddTo (gameObject);;
		}

		private void subscribeBombUse () {
			SubjectProvider.bombSubject.Subscribe (bombStart => {
				if (bombStart) {
					if (GameObject.Find ("bombPoint" + bombNum) != null) {
						GameObject.Find ("bombPoint" + bombNum).SetActive (false);
						bombNum--;
					}
					// if (this != null) {
					GameObject targetCutIn = this.cutIns[this.nowPlayerIndex];
					targetCutInObj = Instantiate (targetCutIn, targetCutIn.transform.position, targetCutIn.transform.rotation);
					StartCoroutine (Utility.DelayMethod (1.5f, EraseCutIn));
				}
			}).AddTo (gameObject);
		}

		void EraseCutIn () {
			if (targetCutInObj != null) {
				Destroy (targetCutInObj);
			}
		}

		void EraseLevelUpCutIn () {
			if (targetLevelUpCutInObj != null) {
				Destroy (targetLevelUpCutInObj);
			}
		}

		public int getBombNum () {
			return bombNum;
		}

		public string getPlayerStatusStr () {
			return (nowPlayerIndex + 1).ToString ("#");
		}
	}
}