using SimpleJSON;

namespace SibcheStoreKit
{
    public abstract class SibchePackage
    {
        public string packageId;
        public string type;
        public string code;
        public string name;
        public string packageDescription;
        public int price;
        public int totalPrice;
        public int discount;
    }

    public class SibcheConsumablePackage : SibchePackage
    {
    }

    public class SibcheNonConsumablePackage : SibchePackage
    {
    }

    public class SibcheSubscriptionPackage : SibchePackage
    {
        public int duration;
        public string group;
    }
}