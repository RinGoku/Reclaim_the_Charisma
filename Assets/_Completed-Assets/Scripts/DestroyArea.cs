using UnityEngine;

namespace CompletedAssets {
	public class DestroyArea : MonoBehaviour {
		void OnTriggerExit2D (Collider2D c) {
			if (c.name != "Gungnir" && c.name != "Gungnir(Clone)" && c.gameObject.tag != "Player") {
				UbhBullet b = c.gameObject.GetComponent<UbhBullet> ();
				if (b != null) {
					UbhObjectPool.instance.ReleaseBullet (b);
				} else {
					Destroy (c.gameObject);
				}
			}
		}
	}
}