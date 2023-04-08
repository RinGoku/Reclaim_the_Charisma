using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CompletedAssets {
	public abstract class Emitter : MonoBehaviour {
		// Waveプレハブを格納する
		public GameObject[] waveFirst;
		public GameObject[] waveMiddleBoss;
		public GameObject[] waveSecond;
		public GameObject[] waveBoss;

		// 各タイミングでのBGM管理
		public string waveFirstBGM;
		public string waveMiddleBossBGM;
		public string waveSecondBGM;
		public string waveBossBGM;

		// 現在のWave
		private int currentWave;

		// Managerコンポーネント
		private Manager manager;
		protected Subject<bool> EndStage = new Subject<bool> ();

		// 初期処理
		protected virtual IEnumerator Start () {
			initialize ();
			// Managerコンポーネントをシーン内から探して取得する
			// manager = FindObjectOfType<Manager> ();
			// // タイトル表示中は待機
			// while (manager.IsPlaying () == false) {
			// 	yield return new WaitForEndOfFrame ();
			// }
			StartCoroutine (Utility.DelayMethod (2.5f, () => {
				GameObject.Find ("DefaultBGM").GetComponent<AudioSource> ().Play ();
			}));
			while (FindObjectOfType<SplashLetters> () != null) {
				yield return new WaitForEndOfFrame ();
			}

			GameObject[][] array = new GameObject[4][] { waveFirst, waveMiddleBoss, waveSecond, waveBoss };
			string[] bgmNames = new string[4] { waveFirstBGM, waveMiddleBossBGM, waveSecondBGM, waveBossBGM };
			int count = 0;
			foreach (GameObject[] wave in array) {
				// Waveが存在しなければコルーチンを終了する
				if (wave.Length == 0) {
					yield break;
				}
				string thisBgmName = bgmNames[count];
				foreach (string bgmName in getBGMNames ()) {
					AudioSource source = GameObject.Find (bgmName).GetComponent<AudioSource> ();
					if (thisBgmName.Equals (bgmName)) {
						source.enabled = true;
						if (!source.isPlaying) {
							source.Play ();
						}
					} else {
						source.enabled = false;
						if (source.isPlaying) {
							source.Stop ();
						}
					}
				}
				while (true) {
					// Waveを作成する
					GameObject g = (GameObject) Instantiate (wave[currentWave], transform.position, Quaternion.identity);

					// WaveをEmitterの子要素にする
					g.transform.parent = transform;

					// 次Waveまでの間隔調整
					// なおWaveオブジェクト自体の削除管理はWave自身が行う
					Wave w = g.GetComponent<Wave> ();
					if (w != null) {
						// Waveスクリプトが付与されているなら、設定されている秒数待機
						yield return new WaitForSeconds (w.waitTimeUntilNext);
					} else {
						// Waveの子要素のEnemyが全て削除されるまで待機する
						while (g != null && g.transform.childCount != 0) {
							yield return new WaitForEndOfFrame ();
						}
					}

					// 格納されているWaveを全て実行したらcurrentWaveを0にする（最初から -> ループ）
					if (wave.Length <= ++currentWave) {
						while (FindObjectOfType<Player> () == null) {
							yield return new WaitForEndOfFrame ();
						}
						while (GameObject.FindGameObjectsWithTag ("Point").Length != 0) {
							yield return new WaitForEndOfFrame ();

						}
						yield return new WaitForSeconds (1f);
						// プレイヤーオブジェクトを操作不能にする
						if ((FindObjectOfType<Player> () != null)) {
							FindObjectOfType<Player> ().makeDisabled ();
						}
						// メッセージを表示
						if (getMessages () != null) {
							FindObjectOfType<Message> ().SetMessagePanel (getMessages () [count]);
							// メッセージパネルが消えるまでは待機
							while (FindObjectOfType<Message> ().GetTransform ().GetChild (0).gameObject.activeSelf) {
								yield return new WaitForEndOfFrame ();
							}
						}
						FindObjectOfType<Player> ().makeEnabled ();
						currentWave = 0;
						count++;
						break;
					}

					yield return new WaitForSeconds (0.1f);
				}
			}
			EndStage.OnNext (true);
		}

		// 以下の情報は各ステージごとに実装するためここでは抽象
		// ・会話パートの内容
		// ・BGMの内容
		// ・初期化処理
		protected abstract TalkInfo[][] getMessages ();
		protected abstract string[] getBGMNames ();
		protected abstract void initialize ();
	}
}