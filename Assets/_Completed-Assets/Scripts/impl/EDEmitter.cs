using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace CompletedAssets {
    public class EDEmitter : Emitter {
        public string[] allBGMNames;
        override protected TalkInfo[][] getMessages () {
            return new TalkInfo[1][] {
                new TalkInfo[] {
                    // new TalkInfo ("illust1", "レミリア「うーうー（最近退屈ね...）」"),
                    //     new TalkInfo ("illust1", "レミリア「うーうー（この体のせいで館の外にも出れなくなっちゃったし）」"),
                    //     new TalkInfo ("illust1", "レミリア「うーうー（私のカリスマ、どこに行っちゃったのかしら）」"),
                    //     new TalkInfo ("illust1", "レミリア「うーうー（...）」"),
                    //     new TalkInfo ("illust1", "レミリア「うーうー（もしかして...）」"),
                    //     new TalkInfo ("illust1", "レミリア「うーうー（私のカリスマ...誰かに盗まれたんじゃ...）」"),
                    //     new TalkInfo ("illust1", "レミリア「うーうー（!!!）」"),
                    //     new TalkInfo ("illust1", "レミリア「うーうー（そうよ...きっとそう！）」"),
                    //     new TalkInfo ("illust1", "レミリア「うーうー（誰が盗んだのか知らないけどただではおかないわ）」"),
                }
            };
        }

        override protected IEnumerator Start () {
            yield return new WaitForSeconds(3);
            FadeManager.FadeOut (9);
        }

        override protected void initialize () {

        }

        override protected string[] getBGMNames () {
            return allBGMNames;
        }
    }
}