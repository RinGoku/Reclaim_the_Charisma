    using System.Collections;
    using UnityEngine.UI;
    using UnityEngine;
    namespace CompletedAssets {

        public class UIController : MonoBehaviour {

            [SerializeField]
            private Canvas canvas;
            [SerializeField]
            private Transform targetTfm;

            [SerializeField]
            private Image image;

            private RectTransform canvasRectTfm;

            private RectTransform imageRectTfm;

            private Vector3 offset = new Vector3 (0, 0, 0);

            void Start () {
                canvasRectTfm = canvas.GetComponent<RectTransform> ();
                imageRectTfm = image.GetComponent<RectTransform> ();
            }

            void Update () {
                if (targetTfm == null) {
                    return;
                }
                Vector3 myPos = targetTfm.position;
                myPos.y -= 20f;
                canvasRectTfm.localPosition = myPos;
                imageRectTfm.localPosition = myPos;
            }

            public void SetTargetTfm (Transform transform) {
                targetTfm = transform;
            }
        }
    }