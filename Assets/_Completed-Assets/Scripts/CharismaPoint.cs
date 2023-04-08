using System;
using System.Collections;
using UniRx;
using UnityEngine;
using UnityEngine.Serialization;

namespace CompletedAssets {
    public class CharismaPoint : PoolableObject {
        [SerializeField]
        private int charismaFlyingTime = 1;
        [SerializeField]
        private float charismaFlyingDelayTime = 2f;
        [SerializeField]
        private int point = 100;
        protected virtual void Start () {
            Pool = Store.charismaPool;
        }

        public override void Init () {
            AddCharismaPoint ();
            Invoke ("DestoryThis", charismaFlyingTime + charismaFlyingDelayTime);
            iTween.MoveTo (gameObject, iTween.Hash ("x", 320.5819, "y", 258.7166, "time", charismaFlyingTime, "delay", charismaFlyingDelayTime));
        }

        void AddCharismaPoint () {
            // 2秒後に加算する
            Observable.Timer (TimeSpan.FromMilliseconds (charismaFlyingDelayTime * 1000))
                .Subscribe (x => {
                    SubjectProvider.charismaPointSubject.OnNext (point);
                })
                .AddTo (this);
        }

        public void DestoryThis () {
            ReturnToPool ();
        }
    }
}