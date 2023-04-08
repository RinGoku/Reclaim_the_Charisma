using System;
using System.Collections;
using UniRx;
using UnityEngine;
using UnityEngine.Serialization;

namespace CompletedAssets {
    public class CharismaPointMini : CharismaPoint {
        protected override void Start () {
            Pool = Store.charismaMiniPool;
        }
    }
}