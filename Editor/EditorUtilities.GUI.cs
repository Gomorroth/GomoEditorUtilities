using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;

namespace gomoru.su
{
    partial class EditorUtilities
    {
        public static class GUI
        {
            private static GUIContent _guiContentSingleton;

            public static GUIContent Text(string text)
            {
                var content = _guiContentSingleton;
                if (content == null)
                {
                    return _guiContentSingleton = new GUIContent(text);
                }
                content.text = text;
                return content;
            }

            public struct DisabledScope : IDisposable
            {
                public DisabledScope(bool disabled)
                {
                    EditorGUI.BeginDisabledGroup(disabled);
                }

                public void Dispose()
                {
                    EditorGUI.EndDisabledGroup();
                }
            }

            public struct FoldoutHeaderGroupScope : IDisposable
            {
                public bool IsOpen;

                public FoldoutHeaderGroupScope(string header, ref bool isOpen) : this(Text(header), ref isOpen)
                { }

                public FoldoutHeaderGroupScope(GUIContent header, ref bool isOpen)
                {
                    IsOpen = isOpen = EditorGUILayout.BeginFoldoutHeaderGroup(isOpen, header);
                    if (isOpen)
                    {
                        EditorGUI.indentLevel++;
                    }
                }

                public void Dispose()
                {
                    if (IsOpen)
                    {
                        EditorGUI.indentLevel--;
                    }
                    EditorGUILayout.EndFoldoutHeaderGroup();
                }
            }

            public struct GroupScope : IDisposable
            {
                private float _originalLabelWidth;

                public GroupScope(string header, float? labelWidth = null) : this(Text(header), labelWidth)
                { }

                public GroupScope(GUIContent header, float? labelWidth = null)
                {
                    GUILayout.Label(header, EditorStyles.boldLabel);
                    _originalLabelWidth = EditorGUIUtility.labelWidth;
                    EditorGUIUtility.labelWidth = labelWidth ?? _originalLabelWidth;

                    EditorGUI.indentLevel++;
                }

                public void Dispose()
                {
                    EditorGUIUtility.labelWidth = _originalLabelWidth;
                    EditorGUI.indentLevel--;
                    EditorGUILayout.Space();
                }
            }
        }
    }
}
