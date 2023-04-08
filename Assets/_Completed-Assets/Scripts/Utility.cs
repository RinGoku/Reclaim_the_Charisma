using System;
using System.Collections;
using UnityEngine;

namespace CompletedAssets {
    public class Utility {

        public static IEnumerator DelayMethod (float waitTime, Action action) {
            yield return new WaitForSeconds (waitTime);
            action ();
        }

        public static void toVisibilityHidden (GameObject target) {
            changeAlpha (target, 0);
        }

        public static void toVisible (GameObject target) {
            changeAlpha (target, 255);
        }

        public static void changeAlpha (GameObject target, float a) {
            SpriteRenderer rend = target.GetComponent<SpriteRenderer> ();
            Color spriteColor = rend.color;
            rend.color = new Color (spriteColor.r, spriteColor.g, spriteColor.b, a);
        }

    }
}