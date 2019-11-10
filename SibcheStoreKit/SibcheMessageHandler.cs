using System;
using System.Collections.Generic;
using SimpleJSON;
using UnityEngine;

namespace SibcheStoreKit
{
    public class SibcheMessageHandler : MonoBehaviour
    {
        public void NotifyLogin(String str)
        {
            try
            {
                JSONNode node = JSON.Parse(str);
                bool isLoginSuccessful = int.Parse(node["isLoginSuccessful"].Value) > 0 ? true : false;
                SibcheError error = node["error"].Value.Length > 0 ? new SibcheError(node["error"].Value) : null;
                var userName = node["userName"].Value;
                var userId = node["userId"].Value;

                Sibche.OnLogin(isLoginSuccessful, error, userName, userId);
            }
            catch (Exception ex)
            {
                Debug.Log("SibcheStoreKit: " + ex.Message);
            }
        }

        public void NotifyLogout(String str)
        {
            try
            {
                Sibche.OnLogout();
            }
            catch (Exception ex)
            {
                Debug.Log("SibcheStoreKit: " + ex.Message);
            }
        }

        public void NotifyFetchPackages(String str)
        {
            try
            {
                JSONNode node = JSON.Parse(str);
                bool isSuccessfull = int.Parse(node["isSuccessful"].Value) > 0 ? true : false;
                SibcheError error = node["error"].Value.Length > 0 ? new SibcheError(node["error"].Value) : null;
                var packagesArray = node["packagesArray"].AsArray;
                List<SibchePackage> packages = new List<SibchePackage>();
                foreach (var item in packagesArray.Children)
                {
                    var package = SibchePackageFactory.GetSibchePackage(item.Value);
                    packages.Add(package);
                }
                Sibche.OnFetchPackages(isSuccessfull, error, packages);
            }
            catch (Exception ex)
            {
                Debug.Log("SibcheStoreKit: " + ex.Message);
            }
        }

        public void NotifyFetchPackage(String str)
        {
            try
            {
                JSONNode node = JSON.Parse(str);
                bool isSuccessfull = int.Parse(node["isSuccessful"].Value) > 0 ? true : false;
                SibcheError error = node["error"].Value.Length > 0 ? new SibcheError(node["error"].Value) : null;
                SibchePackage package = node["package"].Value.Length > 0 ? SibchePackageFactory.GetSibchePackage(node["package"].Value) : null;
                Sibche.OnFetchPackage(isSuccessfull, error, package);
            }
            catch (Exception ex)
            {
                Debug.Log("SibcheStoreKit: " + ex.Message);
            }
        }

        public void NotifyFetchActivePackages(String str)
        {
            try
            {
                JSONNode node = JSON.Parse(str);
                bool isSuccessfull = int.Parse(node["isSuccessful"].Value) > 0 ? true : false;
                SibcheError error = node["error"].Value.Length > 0 ? new SibcheError(node["error"].Value) : null;
                var purchasePackagesArray = node["purchasePackagesArray"].AsArray;
                List<SibchePurchasePackage> purchasePackages = new List<SibchePurchasePackage>();
                foreach (var item in purchasePackagesArray.Children)
                {
                    var purchasePackage = new SibchePurchasePackage(item.Value);
                    purchasePackages.Add(purchasePackage);
                }
                Sibche.OnFetchActivePackages(isSuccessfull, error, purchasePackages);
            }
            catch (Exception ex)
            {
                Debug.Log("SibcheStoreKit: " + ex.Message);
            }
        }

        public void NotifyPurchase(String str)
        {
            try
            {
                JSONNode node = JSON.Parse(str);
                bool isSuccessfull = int.Parse(node["isSuccessful"].Value) > 0 ? true : false;
                SibcheError error = node["error"].Value.Length > 0 ? new SibcheError(node["error"].Value) : null;
                SibchePurchasePackage purchasePackage = node["purchasePackage"].Value.Length > 0 ? new SibchePurchasePackage(node["purchasePackage"].Value) : null;

                Sibche.OnPurchase(isSuccessfull, error, purchasePackage);
            }
            catch (Exception ex)
            {
                Debug.Log("SibcheStoreKit: " + ex.Message);
            }
        }

        public void NotifyConsume(String str)
        {
            try
            {
                JSONNode node = JSON.Parse(str);
                bool isSuccessfull = int.Parse(node["isSuccessful"].Value) > 0 ? true : false;
                SibcheError error = node["error"].Value.Length > 0 ? new SibcheError(node["error"].Value) : null;

                Sibche.OnConsume(isSuccessfull, error);
            }
            catch (Exception ex)
            {
                Debug.Log("SibcheStoreKit: " + ex.Message);
            }
        }

        public void NotifyGetCurrentUserData(String str)
        {
            try
            {
                JSONNode node = JSON.Parse(str);
                bool isSuccessfull = int.Parse(node["isSuccessful"].Value) > 0 ? true : false;
                SibcheError error = node["error"].Value.Length > 0 ? new SibcheError(node["error"].Value) : null;
                LoginStatusType loginStatus;
                Enum.TryParse(node["loginStatus"].Value, out loginStatus);
                string userCellphoneNumber = node["userCellphoneNumber"].Value;
                string userId = node["userId"];

                Sibche.OnGetCurrentUserData(isSuccessfull, error, loginStatus, userCellphoneNumber, userId);
            }
            catch (Exception ex)
            {
                Debug.Log("SibcheStoreKit: " + ex.Message);
            }
        }
    }
}