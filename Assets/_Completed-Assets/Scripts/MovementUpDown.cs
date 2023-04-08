using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CompletedAssets {
    public class MovementUpDown : MonoBehaviour {
        // Start is called before the first frame update
        Spaceship spaceship;

        [SerializeField, Range (0, 1)]
        float time = 1;

        [SerializeField]
        int endY = 100;
        [SerializeField]
        bool isLeft = true;

        [SerializeField]
        bool isCurve = true;

        [SerializeField]
        Sprite spriteAfterCurve;

        private SpriteRenderer spriteRenderer;
        private float startTime;

        private float endTime;
        private Vector3 startPosition;
        private Vector3 endPosition;
        private float bossMoveVolume = 100;

        private bool isPositiveMoveY = true;
        private bool isPositiveMoveX = true;

        private float speed = 200;    

        void OnEnable () {
            spaceship = GetComponent<Spaceship> ();
            if (time <= 0) {
                transform.position = endPosition;
                enabled = false;
                return;
            }
            startTime = Time.timeSinceLevelLoad;
            startPosition = transform.position;
            endPosition = transform.position;
            endPosition.y = endY;
            spaceship.canShot = false;
        }
        IEnumerator Start () {
            spriteRenderer = gameObject.GetComponent<SpriteRenderer> ();
            if (!isLeft) {
                spriteRenderer.flipX = true;
            }
            yield return null;
        }

        // Update is called once per frame
        void Update () {
            Vector3 thisPosition = transform.position;
            if (!spaceship.canShot && thisPosition.y <= 300) {
                spaceship.canShot = true;
            }
            if (isCurve && thisPosition.y <= (endY + 50)) {
                spriteRenderer.sprite = spriteAfterCurve;
                thisPosition.x += isLeft ? -1 * speed * Time.deltaTime : speed * Time.deltaTime;
                if (thisPosition.y >= endY) {
                    thisPosition.y -= speed * Time.deltaTime;
                }
            } else {
                thisPosition.y -= speed * Time.deltaTime;
            }
            transform.position = thisPosition;
        }
    }
}