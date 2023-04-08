using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CompletedAssets {
    public class MovementSanae : MonoBehaviour {
        // Start is called before the first frame update
        Spaceship spaceship;

        [SerializeField, Range (0, 1)]
        float time = 1;

        [SerializeField]
        Vector3 endPosition;

        private float startTime;
        private Vector3 startPosition;

        private float bossMoveVolume = 100;

        private bool isPositiveMoveY = true;
        private bool isPositiveMoveX = true;

        void OnEnable () {
            spaceship = GetComponent<Spaceship> ();
            if (time <= 0) {
                transform.position = endPosition;
                enabled = false;
                return;
            }
            startTime = Time.timeSinceLevelLoad;
            startPosition = transform.position;
            spaceship.canShot = false;
        }
        IEnumerator Start () {
            yield return new WaitForSeconds (10);
            float count = 0;
            int thresholdCount = 0;
            while (true) {
                if (spaceship.canShot) {
                    // yield return new WaitForSeconds (5);
                    Vector3 pos = transform.position;
                    // float vol = Time.deltaTime * -1 * 100;
                    float vol = count / 1000f + 10 * Mathf.Sin (count / 100f);
                    pos.x += (isPositiveMoveX ? 1 : -1) * vol;
                    pos.y += (isPositiveMoveY ? 1 : -1) * vol;
                    bossMoveVolume += vol;
                    transform.position = pos;
                    // if (thresholdCount % 2 == 0 ? pos.y >= 250 : pos.y <= 100) {
                    //     bossMoveVolume = 0;
                    //     isPositiveMoveY = !isPositiveMoveY;
                    //     count = 0;
                    //     if (thresholdCount % 2 == 0) {
                    //         isPositiveMoveX = !isPositiveMoveX;
                    //     }
                    //     thresholdCount++;
                    //     yield return new WaitForSeconds (8);
                    // }
                    // count++;
                }
                yield return null;
            }
        }

        // Update is called once per frame
        void Update () {
            var diff = Time.timeSinceLevelLoad - startTime;
            if (diff > time) {
                if (!spaceship.canShot) {
                    transform.position = endPosition;
                    spaceship.canShot = true;
                }
            } else {
                var rate = diff / time;
                transform.position = Vector3.Lerp (startPosition, endPosition, rate);
            }
        }
    }
}