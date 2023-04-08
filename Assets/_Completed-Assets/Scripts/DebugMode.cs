using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Collections;
using System.Linq;

namespace CompletedAssets {
	public class DebugMode : MonoBehaviour {
		public int fCount = 0;
		public int gCount = 0;
		public bool invincibleMode = false;

		IEnumerator Start () {
			while(true) {
				if (!invincibleMode && Input.GetKey (KeyCode.F)) {
					fCount++;
					if (fCount >= 3) {
						invincibleMode = true;
						fCount = 0;
					}
				}
				if (invincibleMode && Input.GetKey (KeyCode.G)) {
					gCount++;
					if (gCount >= 3) {
						invincibleMode = false;
						gCount = 0;
					}
				}
				yield return null;
			}
		}
	}
}