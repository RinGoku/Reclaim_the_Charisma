using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine.SceneManagement;

namespace CompletedAssets {
    public class Stage2Emitter : Emitter {
        public string[] allBGMNames;
        override protected TalkInfo[][] getMessages () {
            return new TalkInfo[4][] {
                new TalkInfo[] {
                        new TalkInfo ("Remilia", "レミリア", "やっぱり医者にはカリスマ性があるわよね"),
                            new TalkInfo ("Udonge", "鈴仙", "お師匠様に何か用ですか？"),
                            new TalkInfo ("Remilia", "レミリア", "あら月の兎じゃない。あまりカリスマはなさそうだけど"),
                            new TalkInfo ("Udonge", "鈴仙", "その姿を治してもらうおつもりですか？"),
                            new TalkInfo ("Remilia", "レミリア", "治してもらうわけじゃないわ"),
                            new TalkInfo ("Remilia", "レミリア", "自分で治すのよ、力づくでね！"),
                            new TalkInfo ("Udonge", "鈴仙", "吸血鬼はイメージ通り血気盛んなのね")
                    },
                    new TalkInfo[] {
                        new TalkInfo ("Remilia", "レミリア", "あら意外にカリスマ回収できたわね")

                    },
                    new TalkInfo[] {
                        new TalkInfo ("Remilia", "レミリア", "医務室はどこかしら？"),
                            new TalkInfo ("Eirin", "永琳", "厄介な異変に巻き込まれているようね"),
                            new TalkInfo ("Remilia", "レミリア", "あらやっと出てきたわね"),
                            new TalkInfo ("Remilia", "レミリア", "巻き込まれてるも何もあなたのせいじゃない"),
                            new TalkInfo ("Remilia", "レミリア", "私のカリスマを返しなさい"),
                            new TalkInfo ("Eirin", "永琳", "..."),
                            new TalkInfo ("Eirin", "永琳", "これは重症ね"),
                            new TalkInfo ("Eirin", "永琳", "私の薬で治してあげることもできそうだけど？"),
                            new TalkInfo ("Remilia", "レミリア", "薬じゃなくてカリスマをよこしなさいよ"),
                            new TalkInfo ("Eirin", "永琳", "年長者の好意は受け取っておくものよ"),
                            new TalkInfo ("Remilia", "レミリア", "そろそろ介護が必要なんじゃない？")
                    },
                    new TalkInfo[] {
                        new TalkInfo ("Remilia", "レミリア", "口ほどにもないわね"),
                            new TalkInfo ("Remilia", "レミリア", "もっともっとカリスマがほしい...")

                    }
            };
        }

        override protected void initialize () {
            EndStage.Subscribe ((ele) => {
                FadeManager.FadeOut (4);
            }).AddTo (gameObject);
        }

        override protected string[] getBGMNames () {
            return allBGMNames;
        }
    }
}