using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace CompletedAssets {

    public class GameOver : MonoBehaviour {
        void Start () { // ゲーム中ではなく、Xキーが押されたらtrueを返す。
            // if (Input.GetKeyDown (KeyCode.Y)) {
            //     // isGameStart = true;
            //     // GameStart ();
            //     FadeManager.FadeOut (0);
            // }
            StartCoroutine (Utility.DelayMethod (3f, () => {
                FadeManager.FadeOut (0);
            }));
        }
    }
}