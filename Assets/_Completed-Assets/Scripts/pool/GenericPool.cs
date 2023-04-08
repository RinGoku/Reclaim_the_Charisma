using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CompletedAssets {

    public class GenericPool : MonoBehaviour {
        [SerializeField]
        private PoolableObject PooledObject;

        // プールのサイズ（メモリ節約)
        private const int PoolSize = 2000;
        // プールの実態
        private Queue<PoolableObject> _pool;

        void Start () {
            if (_pool != null) {
                _pool.Clear ();
            }
            _pool = new Queue<PoolableObject> (PoolSize);
            for (int i = 0; i < PoolSize; i++) {
                PoolableObject pooled = Instantiate (PooledObject, gameObject.transform.position, gameObject.transform.rotation);
                pooled.gameObject.SetActive (false);
                _pool.Enqueue (pooled);
            }
        }

        /// <summary>
        /// プールにオブジェクトがあればそれを利用します。
        /// 無ければ新たにオブジェクトをInstantiateします。
        /// </summary>
        /// <param name="position"></param>
        public T Place<T> (Vector2 position) where T : PoolableObject {
            return (T) Place (position);
        }

        /// <summary>
        /// プールにオブジェクトがあればそれを利用します。
        /// 無ければ新たにオブジェクトをInstantiateします。
        /// </summary>
        /// <param name="position"></param>
        public PoolableObject Place (Vector2 position) {
            PoolableObject obj = null;
            Debug.Log (_pool);
            if (_pool.Count > 0) {
                Debug.Log ("Que");
                obj = _pool.Dequeue ();
                obj.gameObject.SetActive (true);
                obj.transform.position = position;
                obj.Init ();
            } else {
                Debug.Log ("Instance");
                obj = Instantiate (PooledObject, position, PooledObject.transform.rotation);
                obj.Pool = this;
                obj.Init ();
            }
            return obj;
        }

        /// <summary>
        /// オブジェクトをプールに戻します
        /// </summary>
        /// <param name="obj"></param>
        public void Return (PoolableObject obj) {
            obj.gameObject.SetActive (false);
            _pool.Enqueue (obj);
        }

    }
}