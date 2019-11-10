using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

namespace SibcheStoreKit
{
    public enum LoginStatusType
    {
        LoggedIn = 1,
        LoggedOut = 2,
        HaveTokenButFailedToCheck = 3,
    }

    public class Sibche
    {
#if UNITY_IOS && !UNITY_EDITOR
        [DllImport ("__Internal")]
        private static extern void _SibcheStoreKitInit (string appKey, string appUrlScheme);

        [DllImport ("__Internal")]
        private static extern void _SibcheStoreKitLogin ();

        [DllImport ("__Internal")]
        private static extern void _SibcheStoreKitLogout ();

        [DllImport ("__Internal")]
        private static extern void _SibcheStoreKitFetchInAppPurchasePackages ();

        [DllImport ("__Internal")]
        private static extern void _SibcheStoreKitFetchInAppPurchasePackage (string packageId);

        [DllImport ("__Internal")]
        private static extern void _SibcheStoreKitFetchActiveInAppPurchasePackages ();

        [DllImport ("__Internal")]
        private static extern void _SibcheStoreKitPurchasePackage (string packageId);

        [DllImport ("__Internal")]
        private static extern void _SibcheStoreKitConsumePackage (string purchasePackageId);

        [DllImport ("__Internal")]
        private static extern void _SibcheStoreKitGetCurrentUserData ();
#endif

        private static GameObject sibcheManager;
        private static List<Action<bool, SibcheError, string, string>> loginPool = new List<Action<bool, SibcheError, string, string>>();
        private static List<Action> logoutPool = new List<Action>();
        private static List<Action<bool, SibcheError, List<SibchePackage>>> fetchPackagesPool = new List<Action<bool, SibcheError, List<SibchePackage>>>();
        private static List<Action<bool, SibcheError, SibchePackage>> fetchPackagePool = new List<Action<bool, SibcheError, SibchePackage>>();
        private static List<Action<bool, SibcheError, List<SibchePurchasePackage>>> fetchActivePackagesPool = new List<Action<bool, SibcheError, List<SibchePurchasePackage>>>();
        private static List<Action<bool, SibcheError, SibchePurchasePackage>> purchasePool = new List<Action<bool, SibcheError, SibchePurchasePackage>>();
        private static List<Action<bool, SibcheError>> consumePool = new List<Action<bool, SibcheError>>();
        private static List<Action<bool, SibcheError, LoginStatusType, string, string>> getCurrenUserDataPool = new List<Action<bool, SibcheError, LoginStatusType, string, string>>();

        /// <summary>
        /// Initializes Sibche SDK with the specified key and app open url scheme.
        /// </summary>
        /// <param name="appKey">App-Key extracted from developer panel</param>
        /// <param name="appUrlScheme">Url scheme of app</param>
        public static void Initialize(string appKey, string appUrlScheme)
        {
#if UNITY_IOS && !UNITY_EDITOR
            if (sibcheManager == null) {
                sibcheManager = new GameObject ("SibcheManager");
                UnityEngine.Object.DontDestroyOnLoad (sibcheManager);
                sibcheManager.AddComponent<SibcheMessageHandler> ();
            }
            _SibcheStoreKitInit (appKey, appUrlScheme);
#endif
        }

        /// <summary>
        /// Tries to login with SibcheStoreKit.
        /// </summary>
        /// <param name="callback">Callback that called when task is finished</param>
        public static void Login(Action<bool, SibcheError, string, string> callback)
        {
#if UNITY_IOS && !UNITY_EDITOR
            loginPool.Add (callback);
            _SibcheStoreKitLogin ();
#endif
        }

        /// <summary>
        /// This method logouts user and delete any user related data from SibcheStoreKit
        /// </summary>
        /// <param name="callback">Callback that called when task is finished</param>
        public static void Logout(Action callback)
        {
#if UNITY_IOS && !UNITY_EDITOR
            logoutPool.Add (callback);
            _SibcheStoreKitLogout ();
#endif
        }

        /// <summary>
        /// Fetchs defined packages in developer panel of Sibche for your game
        /// </summary>
        /// <param name="callback">Callback that called when task is finished</param>
        public static void FetchPackages(Action<bool, SibcheError, List<SibchePackage>> callback)
        {
#if UNITY_IOS && !UNITY_EDITOR
            fetchPackagesPool.Add (callback);
            _SibcheStoreKitFetchInAppPurchasePackages ();
#endif
        }

        /// <summary>
        /// Fetch specific package for your game
        /// </summary>
        /// <param name="packageId">Id of package you want to fetch it's data</param>
        /// <param name="callback">Callback that called when task is finished</param>
        public static void FetchPackage(string packageId, Action<bool, SibcheError, SibchePackage> callback)
        {
#if UNITY_IOS && !UNITY_EDITOR
            fetchPackagePool.Add (callback);
            _SibcheStoreKitFetchInAppPurchasePackage (packageId);
#endif
        }

        /// <summary>
        /// Fetchs active (purchased / non consumed) package of current user
        /// </summary>
        /// <param name="callback">Callback that called when task is finished</param>
        public static void FetchActivePackages(Action<bool, SibcheError, List<SibchePurchasePackage>> callback)
        {
#if UNITY_IOS && !UNITY_EDITOR
            fetchActivePackagesPool.Add (callback);
            _SibcheStoreKitFetchActiveInAppPurchasePackages ();
#endif
        }

        /// <summary>
        /// Purchase determined package for current user. If not logged in, we try to login and then purchase that package
        /// </summary>
        /// <param name="packageId">Id of package you want to purchase</param>
        /// <param name="callback">Callback that called when task is finished</param>
        public static void Purchase(string packageId, Action<bool, SibcheError, SibchePurchasePackage> callback)
        {
#if UNITY_IOS && !UNITY_EDITOR
            purchasePool.Add (callback);
            _SibcheStoreKitPurchasePackage (packageId);
#endif
        }

        /// <summary>
        /// This function tries to consume consumable in app purchased package with purchasePackageId
        /// </summary>
        /// <param name="purchasePackageId">Id of purchased (consumable) package which you want to consume that</param>
        /// <param name="callback">Callback that called when task is finished</param>
        public static void Consume(string purchasePackageId, Action<bool, SibcheError> callback)
        {
#if UNITY_IOS && !UNITY_EDITOR
            consumePool.Add (callback);
            _SibcheStoreKitConsumePackage (purchasePackageId);
#endif
        }

        /// <summary>
        /// Gets current user data if logged in
        /// </summary>
        /// <param name="callback">Callback that called when task is finished</param>
        public static void GetCurrentUserData(Action<bool, SibcheError, LoginStatusType, string, string> callback)
        {
#if UNITY_IOS && !UNITY_EDITOR
            getCurrenUserDataPool.Add (callback);
            _SibcheStoreKitGetCurrentUserData ();
#endif
        }

        public static void OnLogin(bool isLoginSuccessful, SibcheError error, string userName, string userId)
        {
            for (int i = loginPool.Count - 1; i >= 0; i--)
            {
                loginPool[i](isLoginSuccessful, error, userName, userId);
                loginPool.RemoveAt(i);
            }
        }

        public static void OnLogout()
        {
            for (int i = logoutPool.Count - 1; i >= 0; i--)
            {
                logoutPool[i]();
                logoutPool.RemoveAt(i);
            }
        }

        public static void OnFetchPackages(bool isSuccessful, SibcheError error, List<SibchePackage> packages)
        {
            for (int i = fetchPackagesPool.Count - 1; i >= 0; i--)
            {
                fetchPackagesPool[i](isSuccessful, error, packages);
                fetchPackagesPool.RemoveAt(i);
            }
        }

        public static void OnFetchPackage(bool isSuccessful, SibcheError error, SibchePackage package)
        {
            for (int i = fetchPackagePool.Count - 1; i >= 0; i--)
            {
                fetchPackagePool[i](isSuccessful, error, package);
                fetchPackagePool.RemoveAt(i);
            }
        }

        public static void OnFetchActivePackages(bool isSuccessful, SibcheError error, List<SibchePurchasePackage> purchasePackages)
        {
            for (int i = fetchActivePackagesPool.Count - 1; i >= 0; i--)
            {
                fetchActivePackagesPool[i](isSuccessful, error, purchasePackages);
                fetchActivePackagesPool.RemoveAt(i);
            }
        }

        public static void OnPurchase(bool isSuccessful, SibcheError error, SibchePurchasePackage purchasePackage)
        {
            for (int i = purchasePool.Count - 1; i >= 0; i--)
            {
                purchasePool[i](isSuccessful, error, purchasePackage);
                purchasePool.RemoveAt(i);
            }
        }

        public static void OnConsume(bool isSuccessful, SibcheError error)
        {
            for (int i = consumePool.Count - 1; i >= 0; i--)
            {
                consumePool[i](isSuccessful, error);
                consumePool.RemoveAt(i);
            }
        }

        public static void OnGetCurrentUserData(bool isSuccessful, SibcheError error, LoginStatusType loginStatus, string userCellphoneNumber, string userId)
        {
            for (int i = getCurrenUserDataPool.Count - 1; i >= 0; i--)
            {
                getCurrenUserDataPool[i](isSuccessful, error, loginStatus, userCellphoneNumber, userId);
                getCurrenUserDataPool.RemoveAt(i);
            }
        }
    }
}