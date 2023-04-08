using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;

namespace CompletedAssets {
    public class Store {
        public static BehaviorSubject<int> bossHp = new BehaviorSubject<int> (0);

        public static GenericPool charismaMiniPool = GameObject.Find ("CharismaPointMiniPool(Clone)").GetComponent<GenericPool> ();
        public static GenericPool charismaPool = GameObject.Find ("CharismaPointPool(Clone)").GetComponent<GenericPool> ();
    }
}