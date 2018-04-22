using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Battlerock
{
    public class CreateScriptableObject
    {
#if UNITY_EDITOR
        [MenuItem("Assets/Create/ScriptableAsset")]
        public static void CreateScriptableAsset<T>(T type) where T : class
        {
            var asset = ScriptableObject.CreateInstance(type.ToString());

            AssetDatabase.CreateAsset(asset, "Assets/" + asset.GetType().ToString() + ".asset");
            AssetDatabase.SaveAssets();
        }
#endif
    }
}