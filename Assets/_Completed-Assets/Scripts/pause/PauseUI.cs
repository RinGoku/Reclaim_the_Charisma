using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CompletedAssets {

    public class PauseUI : MonoBehaviour {

        // Update is called once per frame
        public PauseMode selectedMode;
        private PauseMode[] modes = { PauseMode.Resume, PauseMode.Quit, PauseMode.Title };

        [SerializeField]
        private Text resumeText;
        [SerializeField]
        private Text quitText;
        [SerializeField]
        private Text titleText;

        void Start () {
            selectedMode = PauseMode.Resume;
            updateView ();
        }

        void Update () {
            if (Input.GetKeyDown (KeyCode.Return)) {
                doMode ();
            }
            if (Input.GetKeyDown (KeyCode.RightArrow) || Input.GetKeyDown (KeyCode.DownArrow)) {
                int modeIndex = Array.IndexOf (modes, selectedMode) + 1;
                if (modeIndex > modes.Length - 1) {
                    modeIndex = modes.Length - 1;
                }
                selectedMode = modes[modeIndex];
                updateView ();
            }
            if (Input.GetKeyDown (KeyCode.LeftArrow) || Input.GetKeyDown (KeyCode.UpArrow)) {
                int modeIndex = Array.IndexOf (modes, selectedMode) - 1;
                if (modeIndex < 0) {
                    modeIndex = 0;
                }
                selectedMode = modes[modeIndex];
                updateView ();
            }
        }

        void updateView () {
            // Color resumeColor = new Color (resumeText.color.r, resumeText.color.g, resumeText.color.b, resumeText.color.a);
            // Color quitColor = new Color (quitText.color.r, quitText.color.g, quitText.color.b, quitText.color.a);
            Color resumeColor = resumeText.color;
            Color quitColor = quitText.color;
            Color titleColor = titleText.color;
            switch (selectedMode) {
                case PauseMode.Resume:
                    resumeColor.a = 1f;
                    quitColor.a = 0.5f;
                    titleColor.a = 0.5f;
                    break;
                case PauseMode.Quit:
                    resumeColor.a = 0.5f;
                    quitColor.a = 1f;
                    titleColor.a = 0.5f;
                    break;
                case PauseMode.Title:
                    resumeColor.a = 0.5f;
                    quitColor.a = 0.5f;
                    titleColor.a = 1f;
                    break;
            }

            resumeText.color = resumeColor;
            quitText.color = quitColor;
            titleText.color = titleColor;
        }

        public void resumeGame () {
            GameObject.Find ("StageParent").GetComponent<Pausable> ().pausing = false;
        }

        public void quitGame () {
            UnityEngine.Application.Quit ();
        }

        public void goToTitle () {
            GameObject.Find ("StageParent").GetComponent<Pausable> ().pausing = false;
           FindObjectOfType<Manager> ().GameOver(false);
        }

        public void doMode () {
            switch (selectedMode) {
                case PauseMode.Resume:
                    resumeGame ();
                    break;
                case PauseMode.Quit:
                    quitGame ();
                    break;
                case PauseMode.Title:
                    goToTitle ();
                    break;
            }
        }
    }

    public enum PauseMode {
        Resume,
        Quit,
        Title
    }
}