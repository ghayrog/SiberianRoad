﻿#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace TinyInspector
{
    [CustomPropertyDrawer(typeof(ReadOnlyInspectorAttribute))]
    public sealed class ReadOnlyInspectorDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            GUI.enabled = false;
            EditorGUI.PropertyField(position, property, label);
            GUI.enabled = true;
        }
    }
}
#endif
