using UnityEngine;
using UnityEngine.UI;

namespace CompletedAssets {
	public class SplashLetters : MonoBehaviour {
		public float splashDelay;
		public Text splashText;

		void Start () {
			splashText = gameObject.transform.GetChild (0).gameObject.GetComponent<Text> ();
			iTween.MoveTo (gameObject.transform.GetChild (0).gameObject, iTween.Hash ("x", -123.2, "time", 2));
			iTween.MoveTo (gameObject.transform.GetChild (0).gameObject, iTween.Hash ("x", -123.2 + 600, "time", 2, "delay", 4));
			StartCoroutine (Utility.DelayMethod (splashDelay, () => {
				Destroy (gameObject);
			}));
		}
	}
}