using System;
using SimpleJSON;

namespace SibcheStoreKit {
    public class SibchePurchasePackage {
        public string purchasePackageId;
        public string type;
        public string code;
        public DateTime expireAt;
        public DateTime createdAt;
        public SibchePackage package;

        public SibchePurchasePackage (string json) {
            if (!string.IsNullOrEmpty (json) && json.Length > 0) {
                JSONNode node = JSON.Parse (json);
                purchasePackageId = node["purchasePackageId"].Value;
                type = node["type"].Value;
                code = node["code"].Value;
                var expireAtSecs = long.Parse (node["expireAt"].Value);
                var createdAtSecs = long.Parse (node["createdAt"].Value);

                expireAt = new DateTime (1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Local);
                expireAt = expireAt.AddSeconds (expireAtSecs);
                createdAt = new DateTime (1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Local);
                createdAt = createdAt.AddSeconds (createdAtSecs);
                package = SibchePackageFactory.GetSibchePackage (node["package"].Value);
            }
        }
    }
}