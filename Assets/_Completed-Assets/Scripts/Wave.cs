using System.Collections;
using UnityEngine;
using UnityEngine.UI;
namespace CompletedAssets {
    public class Wave : MonoBehaviour {
        [SerializeField]
        public float waitTimeUntilNext = 0;

        void Update () {
            // 全てのWave内容が撃破された場合は自身を削除する
            if (transform.childCount == 0) {
                Destroy (gameObject);
            }
        }
    }
}