using SimpleJSON;

namespace SibcheStoreKit {
    public class SibcheError {
        public string message;
        public int errorCode;
        public int statusCode;

        public SibcheError (string json) {
            if (!string.IsNullOrEmpty (json) && json.Length > 0) {
                JSONNode node = JSON.Parse (json);
                message = node["message"].Value;
                errorCode = int.Parse (node["errorCode"]);
                statusCode = int.Parse (node["statusCode"].Value);
            }
        }
    }
}