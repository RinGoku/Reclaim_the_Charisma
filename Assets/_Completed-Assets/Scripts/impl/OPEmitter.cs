using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine.SceneManagement;

namespace CompletedAssets {
    public class OPEmitter : Emitter {
        public string[] allBGMNames;
        override protected TalkInfo[][] getMessages () {
            return new TalkInfo[1][] {
                new TalkInfo[] {
                    new TalkInfo ("illust1", "レミリア「うーうー（最近退屈ね...）」"),
                        new TalkInfo ("illust1", "レミリア「うーうー（この体のせいで館の外にも出れなくなっちゃったし）」"),
                        new TalkInfo ("illust1", "レミリア「うーうー（私のカリスマ、どこに行っちゃったのかしら）」"),
                        new TalkInfo ("illust1", "レミリア「うーうー（...）」"),
                        new TalkInfo ("illust1", "レミリア「うーうー（もしかして...）」"),
                        new TalkInfo ("illust1", "レミリア「うーうー（私のカリスマ...誰かに盗まれたんじゃ...）」"),
                        new TalkInfo ("illust1", "レミリア「うーうー（!!!）」"),
                        new TalkInfo ("illust1", "レミリア「うーうー（そうよ...きっとそう！）」"),
                        new TalkInfo ("illust1", "レミリア「うーうー（誰が盗んだのか知らないけどただではおかないわ）」"),
                        new TalkInfo ("illust2", "レミリア「うーうー（こうしちゃいられない...カリスマを取り戻さないと!）」"),
                        new TalkInfo ("illust2", "こうしてレミリアは紅魔館を飛び出したのであった..."),
                }
            };
        }

        override protected IEnumerator Start () {
            FadeManager.FadeIn ();
            FindObjectOfType<Message> ().SetMessagePanel (getMessages () [0], false);
            // メッセージパネルが消えるまでは待機
            while (FindObjectOfType<Message> ().GetTransform ().GetChild (0).gameObject.activeSelf) {
                yield return null;
            }
            FadeManager.FadeOut (2);
        }

        override protected void initialize () {

        }

        override protected string[] getBGMNames () {
            return allBGMNames;
        }
    }
}