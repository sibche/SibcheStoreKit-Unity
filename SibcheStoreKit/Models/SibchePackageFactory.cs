using SimpleJSON;

namespace SibcheStoreKit {
    class SibchePackageFactory {
        public static SibchePackage GetSibchePackage (string json) {
            if (!string.IsNullOrEmpty (json) && json.Length > 0) {
                JSONNode node = JSON.Parse (json);
                var type = node["type"].Value;
                switch (type) {
                    case "ConsumableInAppPackage":
                        return new SibcheConsumablePackage {
                            packageId = node["packageId"].Value,
                                type = "SibcheConsumablePackage",
                                code = node["code"].Value,
                                name = node["name"].Value,
                                packageDescription = node["packageDescription"].Value,
                                price = int.Parse (node["price"].Value),
                                totalPrice = int.Parse (node["totalPrice"].Value),
                                discount = int.Parse (node["discount"].Value)
                        };
                    case "NonConsumableInAppPackage":
                        return new SibcheNonConsumablePackage {
                            packageId = node["packageId"].Value,
                                type = "SibcheNonConsumablePackage",
                                code = node["code"].Value,
                                name = node["name"].Value,
                                packageDescription = node["packageDescription"].Value,
                                price = int.Parse (node["price"].Value),
                                totalPrice = int.Parse (node["totalPrice"].Value),
                                discount = int.Parse (node["discount"].Value)
                        };
                    case "SubscriptionInAppPackage":
                        return new SibcheSubscriptionPackage {
                            packageId = node["packageId"].Value,
                                type = "SibcheSubscriptionPackage",
                                code = node["code"].Value,
                                name = node["name"].Value,
                                packageDescription = node["packageDescription"].Value,
                                price = int.Parse (node["price"].Value),
                                totalPrice = int.Parse (node["totalPrice"].Value),
                                discount = int.Parse (node["discount"].Value),
                                duration = int.Parse (node["duration"].Value),
                                group = node["group"].Value
                        };
                }
            }

            return null;
        }
    }
}