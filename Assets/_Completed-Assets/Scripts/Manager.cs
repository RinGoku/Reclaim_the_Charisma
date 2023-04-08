using UnityEngine;
using UnityEngine.SceneManagement;

namespace CompletedAssets {
	public class Manager : MonoBehaviour {
		// Playerプレハブ
		[SerializeField]
		private GameObject player;

		public static GameObject playerObj;

		// タイトル
		[SerializeField]
		private GameObject gameOver;

		[SerializeField]
		private GameObject bulletPool;

		public static GameObject bulletPoolObj;

		[SerializeField]
		private GameObject charismaPool;

		public static GameObject charismaPoolObj;

		[SerializeField]
		private GameObject charismaMiniPool;

		public static GameObject charismaMiniPoolObj;

		[SerializeField]
		private GameObject scoreArea;

		public static GameObject scoreAreaObj;

		private bool isGameStart = false;

		void Start () {
			FadeManager.FadeIn ();
			// Titleゲームオブジェクトを検索し取得する
			isGameStart = true;
			GameStart ();
		}

		void GameStart () {
			// ゲームスタート時に、プレイヤーを作成する
			if (playerObj == null) {
				playerObj = Instantiate (player, player.transform.position, player.transform.rotation);
				DontDestroyOnLoad (playerObj);
			} else {
				playerObj.GetComponent<PlayerManager> ().refreshNowPlayer (player.transform);
			}
			// if (bulletPoolObj == null) {
			// 	bulletPoolObj = Instantiate (bulletPool, player.transform.position, player.transform.rotation);
			// 	// DontDestroyOnLoad (bulletPoolObj);
			// }
			if (charismaMiniPoolObj == null) {
				charismaMiniPoolObj = Instantiate (charismaMiniPool, player.transform.position, player.transform.rotation);
				DontDestroyOnLoad (charismaMiniPoolObj);
			}
			if (charismaPoolObj == null) {
				charismaPoolObj = Instantiate (charismaPool, player.transform.position, player.transform.rotation);
				DontDestroyOnLoad (charismaPoolObj);
			}
			if (scoreAreaObj == null) {
				scoreAreaObj = Instantiate (scoreArea, new Vector2 (102, -53), player.transform.rotation);
				DontDestroyOnLoad (scoreAreaObj);
			}
		}

		public void GameOver (bool isGameOver = true) {
			// FindObjectOfType<Score> ().Save ();
			// ゲームオーバー時に、タイトルを表示する
			isGameStart = false;
			foreach (Transform n in playerObj.transform) {
				GameObject.Destroy (n.gameObject);
			}
			GameObject[] points = GameObject.FindGameObjectsWithTag ("Point");
			for (int i = 0; i < points.Length; i++) {
				if (points[i].GetComponent<CharismaPoint> () != null) {
					points[i].GetComponent<CharismaPoint> ().DestoryThis ();
				} else {
					points[i].GetComponent<CharismaPointMini> ().DestoryThis ();
				}
			}
			GameObject[] bullets = GameObject.FindGameObjectsWithTag ("Bullet");
			for (int i = 0; i < bullets.Length; i++) {
				bullets[i].GetComponent<Bullet> ().DestroyMe ();
			}
			SubjectProvider.charismaPointSubject.OnNext (0);
            if (isGameOver) {
				FadeManager.FadeOut (7);
			} else {
				FadeManager.FadeOut(0);
			}
            GameObject.Destroy (scoreAreaObj);
			GameObject.Destroy (playerObj);
		}

		public void ContinueNext () {
			// FindObjectOfType<Score> ().Save ();
			// ゲームオーバー時に、タイトルを表示する
			isGameStart = false;
			foreach (Transform n in playerObj.transform) {
				GameObject.Destroy (n.gameObject);
			}
			GameObject[] points = GameObject.FindGameObjectsWithTag ("Point");
			for (int i = 0; i < points.Length; i++) {
				if (points[i].GetComponent<CharismaPoint> () != null) {
					points[i].GetComponent<CharismaPoint> ().DestoryThis ();
				} else {
					points[i].GetComponent<CharismaPointMini> ().DestoryThis ();
				}
			}
			GameObject[] bullets = GameObject.FindGameObjectsWithTag ("Bullet");
			for (int i = 0; i < bullets.Length; i++) {
				bullets[i].GetComponent<Bullet> ().DestroyMe ();
			}
			GameObject.Destroy (scoreAreaObj);
			GameObject.Destroy (playerObj);
			SubjectProvider.charismaPointSubject.OnNext (0);
			FadeManager.FadeOut (8);
		}

		public bool IsPlaying () {
			// ゲーム中かどうかはタイトルの表示/非表示で判断する
			return isGameStart;
		}
	}
}