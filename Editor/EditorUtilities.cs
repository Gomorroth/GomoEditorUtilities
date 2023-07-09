using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine;

namespace gomoru.su
{
    public static partial class EditorUtilities
    {
        public static T AddTo<T>(this T obj, UnityEngine.Object asset) where T : UnityEngine.Object
        {
            AssetDatabase.AddObjectToAsset(obj, asset);
            return obj;
        }

        public static T Create<T>(this T obj, string path) where T : UnityEngine.Object
        {
            AssetDatabase.CreateAsset(obj, path);
            return obj;
        }

        public static T ClearSubAssets<T>(this T obj) where T : UnityEngine.Object
        {
            var path = AssetDatabase.GetAssetPath(obj);
            if (path != null)
            {
                foreach (var asset in AssetDatabase.LoadAllAssetsAtPath(path))
                {
                    if (AssetDatabase.IsSubAsset(asset))
                    {
                        GameObject.DestroyImmediate(asset, true);
                    }
                }
            }
            return obj;
        }

        public static T HideInHierarchy<T>(this T obj) where T : UnityEngine.Object
        {
            obj.hideFlags |= HideFlags.HideInHierarchy;
            return obj;
        }

        public static T UndoGetOrAddComponent<T>(this GameObject obj) where T : Component
        {
            var component = obj.GetComponent<T>();
            if (component == null)
            {
                component = Undo.AddComponent<T>(obj);
            }
            return component;
        }
    }
}