using UnityEditor;

public static class PackageBuilder
{
    [MenuItem("Assets/Build UnityPackage")]
    public static void BuildPackage()
    {
        var assetPaths = new string[]
        {
            "Assets/Middlewares/ThaiFontAdjuster",
            "Assets/Middlewares/ThaiFontAdjusterDemo",
        };

        var packagePath = "ThaiFontAdjuster.unitypackage";
        var options = ExportPackageOptions.Recurse;
        AssetDatabase.ExportPackage(assetPaths, packagePath, options);
    }
}
