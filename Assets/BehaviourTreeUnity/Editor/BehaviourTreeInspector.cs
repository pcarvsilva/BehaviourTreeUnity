using UnityEngine;
using UnityEditor;

namespace AITools
{
    [CustomEditor(typeof(BehaviourTree), true)]
    public class BehaviourTreeInspector : Editor
    {
        public static GUIStyle titleStyle;
        public static GUIStyle subTitleStyle;
        public static GUIStyle boldLabelStyle;

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
        }
    }
}
