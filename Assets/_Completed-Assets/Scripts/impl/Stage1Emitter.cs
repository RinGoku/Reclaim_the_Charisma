using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine.SceneManagement;

namespace CompletedAssets {
    public class Stage1Emitter : Emitter {
        public string[] allBGMNames;
        override protected TalkInfo[][] getMessages () {
            return new TalkInfo[4][] {
                new TalkInfo[] {
                        new TalkInfo ("Remilia", "レミリア", "とりあえず信仰が集まっていそうな神社に向かっているけど..."),
                            new TalkInfo ("Sanae", "早苗", "吸血鬼？がうちの神社になんの用ですか？"),
                            new TalkInfo ("Remilia", "レミリア", "おっと、巫女に出会えるとは好都合ね"),
                            new TalkInfo ("Remilia", "レミリア", "私から奪ったカリスマを返してもらうわよ"),
                            new TalkInfo ("Sanae", "早苗", "？？？"),
                            new TalkInfo ("Sanae", "早苗", "よくわかりませんが、うちの神社に危害を加える気なら容赦はしませんよ")
                    },
                    new TalkInfo[] {
                        new TalkInfo ("Remilia", "レミリア", "うーん、巫女からはあまりカリスマを取り返すことはできなかったわね…")

                    },
                    new TalkInfo[] {
                        new TalkInfo ("Remilia", "レミリア", "もっと大物はいないものかしら"),
                            new TalkInfo ("Kanako", "神奈子", "騒々しいな...今日は祭りの予定はないはずだが..."),
                            new TalkInfo ("Remilia", "レミリア", "あら...カリスマを持っていそうな奴が出てきたわね"),
                            new TalkInfo ("Kanako", "神奈子", "お前...紅魔館の吸血鬼だな？何の用だ？"),
                            new TalkInfo ("Remilia", "レミリア", "要件は言わずともわかるはずよ！"),
                            new TalkInfo ("Remilia", "レミリア", "私のカリスマを返しなさい"),
                            new TalkInfo ("Kanako", "神奈子", "？何の話だ？"),
                            new TalkInfo ("Remilia", "レミリア", "しらを切るつもりね...いいわ"),
                            new TalkInfo ("Remilia", "レミリア", "力づくで取り返す"),
                            new TalkInfo ("Kanako", "神奈子", "よくわからんが、退屈していたところだ"),
                            new TalkInfo ("Kanako", "神奈子", "相手をしてやろう！")
                    },
                    new TalkInfo[] {
                        new TalkInfo ("Remilia", "レミリア", "うーん、そこそこ取り戻せたけど…"),
                            new TalkInfo ("Remilia", "レミリア", "まだまだ足りないわね、次行くわよ！")

                    }
            };
        }

        override protected void initialize () {
            EndStage.Subscribe ((ele) => {
                FadeManager.FadeOut (3);
            }).AddTo (gameObject);
        }

        override protected string[] getBGMNames () {
            return allBGMNames;
        }
    }
}