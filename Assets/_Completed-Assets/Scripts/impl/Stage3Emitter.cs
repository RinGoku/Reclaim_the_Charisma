using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine.SceneManagement;

namespace CompletedAssets {
    public class Stage3Emitter : Emitter {
        public string[] allBGMNames;
        override protected TalkInfo[][] getMessages () {
            return new TalkInfo[4][] {
                new TalkInfo[] {
                    new TalkInfo ("Remilia", "レミリア", "宗教には人が集まるものね"),
                        new TalkInfo ("Koishi", "こいし", "ばぁ！"),
                        new TalkInfo ("Remilia", "レミリア", "！？"),
                        new TalkInfo ("Remilia", "レミリア", "あなたいつの間に！？"),
                        new TalkInfo ("Koishi", "こいし", "えー？最初からいたよー？"),
                        new TalkInfo ("Koishi", "こいし", "なんか面白そうだったから追いかけてたの"),
                        new TalkInfo ("Remilia", "レミリア", "..."),
                        new TalkInfo ("Remilia", "レミリア", "本当にめんどうな能力ね"),
                        new TalkInfo ("Remilia", "レミリア", "あなたからはカリスマもそんなに感じないし、遊んでる暇はないの"),
                        new TalkInfo ("Koishi", "こいし", "えー！でももうついていくのも飽きたし、遊んでよー！"),
                        new TalkInfo ("Remilia", "レミリア", "あなたが勝手についてきたんじゃないの...")
                },
                new TalkInfo[] {
                    new TalkInfo ("Remilia", "レミリア", "うーん、やっぱり奪うなら姉の方かしら")
                },
                new TalkInfo[] {
                    new TalkInfo ("Remilia", "レミリア", "そろそろ出てきてもいいんじゃない？"),
                        new TalkInfo ("Byakuren", "白蓮", "新しい入信者かしら"),
                        new TalkInfo ("Remilia", "レミリア", "宗教自体には興味ないのよ"),
                        new TalkInfo ("Remilia", "レミリア", "興味があるのは人を集められるカリスマ"),
                        new TalkInfo ("Remilia", "レミリア", "そして私が奪われたカリスマよ"),
                        new TalkInfo ("Byakuren", "白蓮", "普遍的なカリスマなど存在しませんよ"),
                        new TalkInfo ("Byakuren", "白蓮", "その人の資質ではなく教えによって人は集まるのです"),
                        new TalkInfo ("Byakuren", "白蓮", "あなたはカリスマを奪われたのではなく、単に大衆に飽きられただけでは？"),
                        new TalkInfo ("Remilia", "レミリア", "あなたの説法に興味はないわ"),
                        new TalkInfo ("Remilia", "レミリア", "四の五の言わずに返しなさい！"),
                },
                new TalkInfo[] {
                    new TalkInfo ("Remilia", "レミリア", "この程度なのね"),
                    new TalkInfo ("Remilia", "レミリア", "私も宗教でも始めようかしら...")            
                }
            };
        }

        override protected void initialize () {
            EndStage.Subscribe ((ele) => {
                FadeManager.FadeOut (5);
            }).AddTo (gameObject);
        }

        override protected string[] getBGMNames () {
            return allBGMNames;
        }
    }
}