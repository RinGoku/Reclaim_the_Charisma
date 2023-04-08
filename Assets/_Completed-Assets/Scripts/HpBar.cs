using System.Collections;
using UnityEngine;
using UnityEngine.UI;
namespace CompletedAssets {
    public class HpBar : MonoBehaviour {

        Slider _slider;
        void Start () {
            // スライダーを取得する
            _slider = GameObject.Find ("Slider").GetComponent<Slider> ();
        }

        public void Init (int hp) {
            gameObject.SetActive (true);
            _slider.maxValue = hp;
            _slider.value = hp;
        }

        public void Hide () {
            gameObject.SetActive (false);
        }

        public void UpdateHp (int hp) {
            _slider.value = hp;
        }

        public float GetNowHp () {
            return _slider.value;
        }
    }
}