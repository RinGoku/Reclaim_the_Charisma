using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CompletedAssets {
    public class MovementHorizontal : MonoBehaviour {
        public float speed;

        private Vector3 thisPosition;

        public Spaceship spaceship;

        public bool isRightMove;

        void Start () {
            spaceship = GetComponent<Spaceship> ();
            if (spaceship != null) {
                spaceship.canShot = false;
            }
            // Move (transform.up * -1);
        }

        void Update () {
            thisPosition = transform.position;
            // プレイヤーの座標を取得
            Vector2 pos = transform.position;
            Vector2 targetVector;
            bool canShot;
            if (isRightMove) {
                targetVector = Camera.main.ViewportToWorldPoint (new Vector2 (0, 0));
                thisPosition.x += speed * Time.deltaTime;
                canShot = pos.x <= (targetVector.x - 32);
            } else {
                targetVector = Camera.main.ViewportToWorldPoint (new Vector2 (1, 1));
                thisPosition.x -= speed * Time.deltaTime;
                canShot = pos.x >= (targetVector.x - 32);
            }
            if (canShot && spaceship != null) {
                spaceship.canShot = true;
            }
            transform.position = thisPosition;
        }
    }
}