using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine.SceneManagement;

namespace CompletedAssets {
    public class Stage4Emitter : Emitter {
        public string[] allBGMNames;
        override protected TalkInfo[][] getMessages () {
            return new TalkInfo[4][] {
                new TalkInfo[] {
                        new TalkInfo ("Remilia", "レミリア", "死者の崇拝を集めるのもひとつの手かしら"),
                        new TalkInfo ("Remilia", "レミリア", "今生きているものより今までに死んだものの方が多いものね"),
                            new TalkInfo ("Komachi", "小町", "こんなところに何の用だい？"),
                            new TalkInfo ("Remilia", "レミリア", "うーん..."),
                            new TalkInfo ("Remilia", "レミリア", "あまりカリスマを感じないわね"),
                            new TalkInfo ("Remilia", "レミリア", "あなたの上司の方がいいカリスマを奪えそう"),
                            new TalkInfo ("Komachi", "小町", "よくわからないが、平和的じゃあなさそうだね"),
                            new TalkInfo ("Komachi", "小町", "あんまり仕事の邪魔をされると困るんだ"),
                            new TalkInfo ("Komachi", "小町", "うちの上司は怖いんでね"),
                            new TalkInfo ("Remilia", "レミリア", "その上司もこれからいなくなるんだし安心して道を譲りなさい"),
                    },
                    new TalkInfo[] {
                        new TalkInfo ("Remilia", "レミリア", "なかなかいい味じゃない")
                    },
                    new TalkInfo[] {
                            new TalkInfo ("Remilia", "レミリア", "あんまり川の近くにいたくないわね"),
                            new TalkInfo ("Eiki", "映姫", "吸血鬼の性ですか"),
                            new TalkInfo ("Remilia", "レミリア", "出たわね閻魔"),
                            new TalkInfo ("Remilia", "レミリア", "あなたも私のカリスマを奪ったのかしら？"),
                            new TalkInfo ("Eiki", "映姫", "この私相手に冗談ですか？"),
                            new TalkInfo ("Remilia", "レミリア", "まさか、あなたに嘘などつけるはずもない"),
                            new TalkInfo ("Remilia", "レミリア", "本心であなたを疑っているのよ"),
                            new TalkInfo ("Eiki", "映姫", "それを本心で言っているということは即ち狂人ということでしょうか"),
                            new TalkInfo ("Remilia", "レミリア", "行き過ぎたカリスマは時には狂っているかのように見えるものよ"),
                            new TalkInfo ("Eiki", "映姫", "これは手遅れですね"),
                            new TalkInfo ("Eiki", "映姫", "人から求められたいという欲望、行き過ぎれば身を滅ぼしますよ"),
                            new TalkInfo ("Eiki", "映姫", "そう、あなたは少し欲深すぎる"),
                            new TalkInfo ("Remilia", "レミリア", "説教は結構よ"),
                            new TalkInfo ("Remilia", "レミリア", "さっさとあなたのカリスマを引き渡してもらう"),
                    },
                    new TalkInfo[] {
                        new TalkInfo ("Remilia", "レミリア", "なんだかんだ言っても勝った方が白なのよ"),
                    }
            };
        }

        override protected void initialize () {
            EndStage.Subscribe ((ele) => {
                FadeManager.FadeOut (6);
            }).AddTo (gameObject);
        }

        override protected string[] getBGMNames () {
            return allBGMNames;
        }
    }
}