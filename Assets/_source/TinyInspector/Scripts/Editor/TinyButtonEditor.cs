#if UNITY_EDITOR
using System.Reflection;
using UnityEngine;
using UnityEditor;
using TinyDI;

namespace TinyInspector
{
    [CustomEditor(typeof(TinyButton))]
    public class TinyButtonEditor : Editor
    { 
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            TinyButton button = (TinyButton)target;
            if (button.TargetMonoBehaviour == null) return;

            var targetMonobeh = button.TargetMonoBehaviour;
            var methods = ReflectionTools.GetMethods(targetMonobeh);

            foreach (MethodInfo method in methods)
            {
                if (method.IsDefined(typeof(TinyButtonAttribute)))
                {
                    if (GUILayout.Button(method.Name))
                    {
                        method.Invoke(targetMonobeh, new object[] { });
                    }
                }

                if (method.IsDefined(typeof(TinyPlayButtonAttribute)))
                {
                    if (Application.isPlaying && GUILayout.Button(method.Name))
                    {
                        method.Invoke(targetMonobeh, new object[] { });
                    }
                }
            }
        }
    }
}
#endif