using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine.SceneManagement;

namespace CompletedAssets {
    public class Stage5Emitter : Emitter {
        public string[] allBGMNames;
        override protected TalkInfo[][] getMessages () {
            return new TalkInfo[4][] {
                new TalkInfo[] {
                        new TalkInfo ("Remilia", "レミリア", "彼岸まで来たんだからここにも来ないとね"),
                            new TalkInfo ("Youmu", "妖夢", "白玉楼に何用ですか？"),
                            new TalkInfo ("Remilia", "レミリア", "客人に随分不躾ね"),
                            new TalkInfo ("Youmu", "妖夢", "失礼、しかし悪い気を感じたもので"),
                            new TalkInfo ("Remilia", "レミリア", "あらあら悪いことをしているのはそっちなのにね"),
                            new TalkInfo ("Youmu", "妖夢", "なんのことですか？"),
                            new TalkInfo ("Remilia", "レミリア", "あなたじゃ話にならないわね"),
                            new TalkInfo ("Remilia", "レミリア", "主人を呼んでもらおうかしら"),
                            new TalkInfo ("Youmu", "妖夢", "...今のあなたを幽々子様に会わせるわけにはいきません"),
                            new TalkInfo ("Youmu", "妖夢", "お帰り願います！"),
                            new TalkInfo ("Remilia", "レミリア", "さっさと終わらせるわね")
                    },
                    new TalkInfo[] {
                        new TalkInfo ("Remilia", "レミリア", "そこそこ骨のあるやつだったわね")
                    },
                    new TalkInfo[] {
                        new TalkInfo ("Remilia", "レミリア", "随分長い階段ね"),
                            new TalkInfo ("Yuyuko", "幽々子", "屋敷には通さないわよ"),
                            new TalkInfo ("Yuyuko", "幽々子", "暴れられても困るもの"),
                            new TalkInfo ("Remilia", "レミリア", "そっちから来てくれるならこちらとしては好都合ね"),
                            new TalkInfo ("Remilia", "レミリア", "私のカリスマを返しなさい"),
                            new TalkInfo ("Yuyuko", "幽々子", "紫が言ってた通りね"),
                            new TalkInfo ("Yuyuko", "幽々子", "ほんとにここまで来るなんて"),
                            new TalkInfo ("Remilia", "レミリア", "なんのことよ？"),
                            new TalkInfo ("Remilia", "レミリア", "何か知ってるの？"),
                            new TalkInfo ("Yuyuko", "幽々子", "あなたの今の状態は危険だったのよ"),
                            new TalkInfo ("Remilia", "幽々子", "自分にはカリスマがないんじゃないかって思い込みがね"),
                            new TalkInfo ("Remilia", "レミリア", "何を言っているのかわからないわ"),
                            new TalkInfo ("Yuyuko", "幽々子", "あなたを抑えつけてから話をしましょうか"),
                            new TalkInfo ("Remilia", "レミリア", "もうそんなことどうでもいいのよ"),
                            new TalkInfo ("Remilia", "レミリア", "私のカリスマが取り戻せればね！"),
                    },
                    new TalkInfo[] {
                        new TalkInfo ("Remilia", "レミリア", "もっともっとカリスマが欲しい..."),
                        new TalkInfo ("Yukari", "？？？", "想像以上だったわね"),
                        new TalkInfo ("Remilia", "レミリア", "?!誰?"),
                        new TalkInfo ("Yukari", "？？？", "あなたのカリスマを渇望する底知れない感情、それが近々幻想郷を脅かすような異変につながる可能性があった"),
                        new TalkInfo ("Yukari", "？？？", "だから先手を打ったの"),
                        new TalkInfo ("Remilia", "レミリア", "なんのことよ？"),
                        new TalkInfo ("Remilia", "？？？", "幻想郷内の何人かに協力してもらってあなたのカリスマへの欲望を鎮めようとする粋なイベントよ"),
                        new TalkInfo ("Remilia", "？？？", "カリスマを渇望する感情の裏側で自分にカリスマがないと思い込み、そのせいで惨めな姿に成り果てたあなたには自信を持ってもらう必要があった"),
                        new TalkInfo ("Remilia", "？？？", "自分にはちゃんとカリスマがあるのだと"),
                        new TalkInfo ("Remilia", "？？？", "ただあなたの欲望は私の予想を超えていたようね"),
                        new TalkInfo ("Remilia", "？？？", "結果的にはあなたは暴走し、新しい異変を生み出してしまった"),
                        new TalkInfo ("Remilia", "？？？", "あなたはここで私が止めるわ"),
                        new TalkInfo ("Remilia", "レミリア", "よくわからないけど私が欲しいのはカリスマなの"),
                        new TalkInfo ("Remilia", "レミリア", "それを阻むなら容赦はしないわ"),
                        new TalkInfo ("Remilia", "レミリア", "私は私が失くしたカリスマを取り戻してみせる！"),
                    }
            };
        }

        override protected void initialize () {
            EndStage.Subscribe ((ele) => {
                FindObjectOfType<Manager> ().ContinueNext ();
            }).AddTo (gameObject);
        }

        override protected string[] getBGMNames () {
            return allBGMNames;
        }
    }
}