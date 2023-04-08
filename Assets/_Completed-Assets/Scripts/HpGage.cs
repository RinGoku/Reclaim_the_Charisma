using System.Collections;
using UnityEngine;
using UnityEngine.UI;
namespace CompletedAssets {
    public class HpGage : MonoBehaviour {

        Image gage;

        Text nowHpText;

        Text nameText;

        void Start () {
            // ゲージを取得する
            gage = gameObject.GetComponent<Image> ();
            gage.fillAmount = 1;
            // nowHpText = gameObject.transform.GetChild (0).gameObject.GetComponent<Text> ();
        }

        public void UpdateHp (int nowHp, int maxHp) {
            if (gage != null) {
                gage.fillAmount = (float) nowHp / (float) maxHp;
                // nowHpText.text = nowHp + "/" + maxHp;
            }
        }

        public void SetName (string name) {
            nameText = gameObject.transform.GetChild (1).gameObject.GetComponent<Text> ();
            nameText.text = name;
        }
    }
}