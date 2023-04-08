using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CompletedAssets {

    public class TitleButton : MonoBehaviour {

        public static Subject<bool> selectButton = new Subject<bool> ();

        void Start () {
            selectButton.Subscribe (isSelect => {
                playSE ();
            }).AddTo (gameObject);
        }

        public void OnClickStart () {
            Title.startGame ();
        }

        public void OnClickEnd () {
            Title.quitGame ();
        }

        public void HoverStart () {
            GetComponent<AudioSource> ().Play ();
            Title.selectedMode = Title.TitleMode.Start;
        }

        public void HoverQuit () {
            GetComponent<AudioSource> ().Play ();
            Title.selectedMode = Title.TitleMode.Quit;
        }

        private void playSE () {
            GetComponent<AudioSource> ().Play ();
        }
    }
}