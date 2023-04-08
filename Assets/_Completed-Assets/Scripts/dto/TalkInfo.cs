namespace CompletedAssets {
    public class TalkInfo {
        public string characterName;
        public string displayCharacterName;
        public string message;

        public TalkInfo (string characterName, string displayCharacterName, string message) {
            this.characterName = characterName;
            this.displayCharacterName = displayCharacterName;
            this.message = message;
        }

        public TalkInfo (string characterName, string message) {
            this.characterName = characterName;
            this.displayCharacterName = null;
            this.message = message;
        }
    }
}