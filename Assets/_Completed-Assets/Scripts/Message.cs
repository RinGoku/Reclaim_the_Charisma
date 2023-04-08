using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CompletedAssets {
	public class Message : MonoBehaviour {

		//　メッセージUI
		private Text messageText;
		//　表示するメッセージ
		private TalkInfo[] message;
		//　1回のメッセージの最大文字数
		[SerializeField]
		private int maxTextLength = 90;
		//　1回のメッセージの現在の文字数
		private int textLength = 0;
		//　メッセージの最大行数
		[SerializeField]
		private int maxLine = 3;
		//　現在の行
		private int nowLine = 0;
		//　テキストスピード
		[SerializeField]
		private float textSpeed = 0;
		//　経過時間
		private float elapsedTime = 0f;
		//　今見ている文字番号
		private int nowTextNum = 0;
		//　マウスクリックを促すアイコン
		private Image clickIcon;
		//　クリックアイコンの点滅秒数
		[SerializeField]
		private float clickFlashTime = 0.2f;
		//　1回分のメッセージを表示したかどうか
		private bool isOneMessage = false;
		//　メッセージをすべて表示したかどうか
		private bool isEndMessage = true;

		private bool isStage;

		private Transform thisTransform;

		private Dictionary<string, GameObject> characterMap = new Dictionary<string, GameObject> ();

		void Start () {
			messageText = GetComponentInChildren<Text> ();
			messageText.text = "";
			transform.GetChild (0).gameObject.SetActive (false);
			thisTransform = transform;
		}

		IEnumerator DisplayMessage () {
			string state = GameObject.FindGameObjectWithTag ("PlayerManager").GetComponent<PlayerManager> ().getPlayerStatusStr ();
			foreach (TalkInfo thisMessage in message) {
				if ("Remilia".Equals (thisMessage.characterName) && "1".Equals (state)) {
					messageText.text = thisMessage.displayCharacterName + "「うーうー(" + thisMessage.message + ")」";
				} else {
					messageText.text = thisMessage.displayCharacterName + "「" + thisMessage.message + "」";
				}
				GameObject character;

				if ("Remilia".Equals (thisMessage.characterName)) {
					string thisRemiliaName = switchIfRemilia (thisMessage.characterName, state);
					if (!characterMap.ContainsKey (thisRemiliaName)) {
						characterMap.Add (thisRemiliaName, transform.Find ("Panel/" + thisRemiliaName).gameObject);
					}
					character = characterMap[thisRemiliaName];
				} else {
					if (!characterMap.ContainsKey (thisMessage.characterName)) {
						characterMap.Add (thisMessage.characterName, transform.Find ("Panel/" + thisMessage.characterName).gameObject);
					}
					character = characterMap[thisMessage.characterName];
				}
				SpriteRenderer colorInfo = character.GetComponent<SpriteRenderer> ();
				colorInfo.color = new Color (255, 255, 255, 1);
				character.SetActive (true);
				yield return new WaitUntil (() => Input.GetMouseButtonDown (0) || Input.GetKeyDown (KeyCode.Return));
				yield return null;
				colorInfo.color = new Color (255, 255, 255, 0.5f);
			}
			nowTextNum = 0;
			isEndMessage = true;
			foreach (string key in characterMap.Keys) {
				characterMap[key].SetActive (false);
			}
			transform.GetChild (0).gameObject.SetActive (false);
		}

		IEnumerator DisplayMessageExceptStage () {
			string prevName = "";
			foreach (TalkInfo thisMessage in message) {
				if (!prevName.Equals (thisMessage.characterName)) {
					SpriteRenderer colorInfo = GameObject.Find (thisMessage.characterName).GetComponent<SpriteRenderer> ();
					colorInfo.color = new Color (255, 255, 255, 255);
					if (GameObject.Find (prevName) != null) {
						colorInfo = GameObject.Find (prevName).GetComponent<SpriteRenderer> ();
						colorInfo.color = new Color (255, 255, 255, 0);
					}
					prevName = thisMessage.characterName;
				}
				messageText.text = thisMessage.message;
				yield return new WaitUntil (() => Input.GetMouseButtonDown (0) || Input.GetKeyDown (KeyCode.Return));
				yield return null;
			}
			isEndMessage = true;
			transform.GetChild (0).gameObject.SetActive (false);
		}
		void Update () {
			//　メッセージが終わっていない、または設定されている
			if (isEndMessage) {
				return;
			}
			if (isStage) {
				StartCoroutine (DisplayMessage ());
			} else {
				// ステージ以外(OPとか)はこっち
				StartCoroutine (DisplayMessageExceptStage ());
			}
			isEndMessage = true;
		}

		public bool isDisplayMessage () {
			return isEndMessage;
		}

		public Transform GetTransform () {
			return thisTransform;
		}

		void SetMessage (TalkInfo[] message) {
			this.message = message;
		}
		//　他のスクリプトから新しいメッセージを設定
		public void SetMessagePanel (TalkInfo[] message, bool isStageP = true) {
			SetMessage (message);
			transform.GetChild (0).gameObject.SetActive (true);
			isEndMessage = false;
			isStage = isStageP;
		}

		public string switchIfRemilia (string name, string state) {
			return name + state;
		}
	}
}