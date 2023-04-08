using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CompletedAssets {
    public class MovementLinear : MonoBehaviour {
        public float speed;

        private Vector3 thisPosition;

        public Spaceship spaceship;

        void Start () {
            spaceship = GetComponent<Spaceship> ();
            if (spaceship != null) {
                spaceship.canShot = false;
            }
            // Move (transform.up * -1);
        }

        void Update () {
            thisPosition = transform.position;
            // 画面右上のワールド座標をビューポートから取得
            Vector2 max = Camera.main.ViewportToWorldPoint (new Vector2 (1, 1));
            // プレイヤーの座標を取得
            Vector2 pos = transform.position;
            if (pos.y <= (max.y - 32) && spaceship != null) {
                spaceship.canShot = true;
            }
            thisPosition.y -= speed * Time.deltaTime;
            transform.position = thisPosition;
        }
    }
}