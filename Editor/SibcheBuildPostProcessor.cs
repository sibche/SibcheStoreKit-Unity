using System.IO;
using UnityEditor;
using UnityEditor.Callbacks;

#if UNITY_IOS
using UnityEditor.iOS.Xcode;
using UnityEditor.iOS.Xcode.Extensions;
#endif

public class BuildPostProcessor
{
    [PostProcessBuild]
    public static void ChangeXcodePlist(BuildTarget buildTarget, string path)
    {
        // Part1: Add custom url scheme for app (Replace testapp with your customized url!)
        AddCustomUrlScheme(path, "testapp", "test");

        // Part2: Add Sibche storekit as embed framework
        AddSibcheFrameworkAsEmbed(path);
    }

    /// <summary>
    /// Add custom url scheme to info.plist
    /// </summary>
    /// <param name="path">Root path of built ios project</param>
    /// <param name="customUrlScheme">Url scheme used by your app</param>
    /// <param name="customUrlName">Url scheme name used by your app (this name is not used anywhere)</param>
    static void AddCustomUrlScheme(string path, string customUrlScheme, string customUrlName){
#if UNITY_IOS
        string plistPath = path + "/Info.plist";
        PlistDocument plist = new PlistDocument();
        plist.ReadFromFile(plistPath);

        PlistElementDict rootDict = plist.root;
        PlistElementArray urlArray = rootDict.CreateArray("CFBundleURLTypes");

        PlistElementDict dict = urlArray.AddDict();
        dict.SetString("CFBundleTypeRole", "Editor");
        dict.SetString("CFBundleURLName", customUrlName);

        PlistElementArray urlArrayInner = dict.CreateArray("CFBundleURLSchemes");
        urlArrayInner.AddString(customUrlScheme);

        File.WriteAllText(plistPath, plist.WriteToString());
#endif
    }

    /// <summary>
    /// Add SibcheStoreKit's framework file as embedded framework (dynamic load)
    /// </summary>
    /// <param name="path">Root path of built ios project</param>
    private static void AddSibcheFrameworkAsEmbed(string path){
#if UNITY_IOS
        var projFile = PBXProject.GetPBXProjectPath(path);
        var proj = new PBXProject();

        proj.ReadFromFile(projFile);

        var targetGuid = proj.TargetGuidByName(PBXProject.GetUnityTargetName());
        var fmwkGuid = proj.FindFileGuidByProjectPath("Frameworks/Plugins/iOS/SibcheStoreKit/SibcheStoreKit.framework");
        proj.AddFileToEmbedFrameworks(targetGuid, fmwkGuid);
        proj.AddBuildProperty(targetGuid, "LD_RUNPATH_SEARCH_PATHS", "@executable_path/Frameworks");

        proj.WriteToFile(projFile);
#endif
    }
}