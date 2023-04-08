using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CompletedAssets {

    public class Title : MonoBehaviour {
        // Start is called before the first frame update
        public static TitleMode selectedMode;
        private TitleMode[] modes = { TitleMode.Start, TitleMode.Quit };
        private Subject<TitleMode> modeSubject = new Subject<TitleMode> ();
        IEnumerator Start () {
            FadeManager.FadeIn ();
            subscribeTitleMode ();
            modeSubject.OnNext (TitleMode.Start);
            while (true) {
                if (Input.GetKeyDown (KeyCode.Return)) {
                    doMode ();
                }
                if (Input.GetKeyDown (KeyCode.RightArrow) || Input.GetKeyDown (KeyCode.DownArrow)) {
                    int modeIndex = Array.IndexOf (modes, selectedMode) + 1;
                    if (modeIndex > modes.Length - 1) {
                        modeIndex = modes.Length - 1;
                    }
                    modeSubject.OnNext (modes[modeIndex]);
                    TitleButton.selectButton.OnNext (true);
                }
                if (Input.GetKeyDown (KeyCode.LeftArrow) || Input.GetKeyDown (KeyCode.UpArrow)) {
                    int modeIndex = Array.IndexOf (modes, selectedMode) - 1;
                    if (modeIndex < 0) {
                        modeIndex = 0;
                    }
                    modeSubject.OnNext (modes[modeIndex]);
                    TitleButton.selectButton.OnNext (true);
                }
                yield return null;
            }
        }

        public static void doMode () {
            switch (selectedMode) {
                case TitleMode.Start:
                    startGame ();
                    break;
                case TitleMode.Quit:
                    quitGame ();
                    break;
            }
        }

        public static void startGame () {
            FadeManager.FadeOut (1);
        }

        public static void quitGame () {
            UnityEngine.Application.Quit ();
        }

        private void subscribeTitleMode () {
            modeSubject.Subscribe (ele => {
                selectedMode = ele;
                String targetTagName = "";
                switch (selectedMode) {
                    case TitleMode.Start:
                        targetTagName = "TitleStartIcon";
                        break;
                    case TitleMode.Quit:
                        targetTagName = "TitleEndIcon";
                        break;
                }
                switchVisibleIcon (targetTagName, "TitleStartIcon");
                switchVisibleIcon (targetTagName, "TitleEndIcon");
            }).AddTo (gameObject);
        }

        private void switchVisibleIcon (String targetTagName, String nowTagName) {
            GameObject[] targets = GameObject.FindGameObjectsWithTag (nowTagName);
            foreach (GameObject target in targets) {
                if (targetTagName.Equals (nowTagName)) {
                    Utility.toVisible (target);
                } else {
                    Utility.toVisibilityHidden (target);
                }
            }
        }

        public enum TitleMode {
            Start,
            Quit
        }
    }
}