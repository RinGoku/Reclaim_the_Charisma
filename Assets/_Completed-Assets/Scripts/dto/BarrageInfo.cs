using UnityEngine;

namespace CompletedAssets {
    [System.Serializable]
    public class BarrangeInfo {
        [SerializeField]
        public string barrangeName;
        [SerializeField]
        public string[] shotNames;

        public BarrangeInfo (string barrangeName, string[] shotNames) {
            this.barrangeName = barrangeName;
            this.shotNames = shotNames;
        }
    }
}