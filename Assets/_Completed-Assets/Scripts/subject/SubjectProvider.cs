using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;

namespace CompletedAssets {
    public class SubjectProvider {
        /**ボス戦突入 */
        public static Subject<string> bossBattleStart = new Subject<string> ();
        public static Subject<string> bossBattleEnd = new Subject<string> ();

        /**弾幕切り替え */
        public static Subject<BarrangeInfo> barrangeSwitched = new Subject<BarrangeInfo> ();

        public static Subject<int> charismaPointSubject = new Subject<int> ();

        public static Subject<int> deathSubject = new Subject<int> ();

        /** ボム発射イベント (ボム発射中は敵弾を消すため、Subscribe時にOnNextした最後の値を受け取れるようにBehaviorSubjectにする) */
        public static ReactiveProperty<bool> bombSubject = new ReactiveProperty<bool>(false);

        public static Subject<bool> collisionBullet = new Subject<bool> ();

        public static Subject<bool> pauseEnd = new Subject<bool> ();

    }
}