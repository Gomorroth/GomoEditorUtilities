using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using VRC.SDK3.Avatars.Components;

namespace gomoru.su
{
    public static class RuntimeUtilities
    {
        public static IEnumerable<Transform> EnumerateChildren(this Transform tr)
        {
            int count = tr.childCount;
            for (int i = 0; i < count; i++)
            {
                yield return tr.GetChild(i);
            }
        }

        public static IEnumerable<string> EnumeratePropertyNames(this Shader shader) => Enumerable.Range(0, shader.GetPropertyCount()).Select(shader.GetPropertyName);

        public static VRCAvatarDescriptor FindAvatarFromParent(this GameObject obj)
        {
            var tr = obj.transform;
            VRCAvatarDescriptor avatar = null;
            while (tr != null && (avatar = tr.GetComponent<VRCAvatarDescriptor>()) == null)
            {
                tr = tr.parent;
            }
            return avatar;
        }

        public static GameObject GetOrAddChild(this GameObject obj, string name)
        {
            var c = obj.transform.EnumerateChildren().FirstOrDefault(x => x.name == name)?.gameObject;
            if (c == null)
            {
                c = new GameObject(name);
                c.transform.parent = obj.transform;
            }
            return c;
        }

        public static string GetRelativePath(this GameObject obj, GameObject root, bool includeRelativeTo = false) => GetRelativePath(obj.transform, root.transform, includeRelativeTo);

        public static string GetRelativePath(this Transform transform, Transform root, bool includeRelativeTo = false)
        {
            var buffer = _relativePathBuffer;
            if (buffer is null)
            {
                buffer = _relativePathBuffer = new string[128];
            }

            var t = transform;
            int idx = buffer.Length;
            while (t != null && t != root)
            {
                buffer[--idx] = t.name;
                t = t.parent;
            }
            if (includeRelativeTo && t != null && t == root)
            {
                buffer[--idx] = t.name;
            }

            return string.Join("/", buffer, idx, buffer.Length - idx);
        }

        private static string[] _relativePathBuffer;

        public static bool TryGetComponentInChildren<T>(this Component component, out T result) where T : Component => component.gameObject.TryGetComponentInChildren(out result);

        public static bool TryGetComponentInChildren<T>(this GameObject obj, out T result) where T : Component
        {
            result = obj.GetComponentInChildren<T>();
            return result != null;
        }
    }
}