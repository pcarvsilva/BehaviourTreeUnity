using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using NodeEditorFramework;
using NodeEditorFramework.Standard;
using NodeEditorFramework.Utilities;

namespace AITools
{
    [CustomEditor(typeof(BehaviourTreeAgent), true)]
    public class BehaviourTreeAgentInspector : Editor
    {
        bool objectFold;
        bool floatFold;
        bool integerFold;
        bool boolFold;
        bool vectorFold;

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            BehaviourTreeAgent agent = target as BehaviourTreeAgent;
            if (agent.tree == null) return;
            if (NodeEditor.curNodeCanvas == null || NodeEditor.curNodeCanvas.savePath != agent.tree.savePath)
            {
                BehaviourTree.selectedAgent = agent;
                NodeEditor.ReInit(false);
                NodeCanvas c = NodeEditorSaveManager.CreateWorkingCopy(agent.tree);
                NodeEditor.curNodeCanvas = c;
                NodeEditorWindow.editor.canvasCache.SetCanvas(c);
            }
            if (agent.gameObjectParameters != null)
            {
                objectFold = EditorGUILayout.Foldout(objectFold, "Game Objects");
                if (objectFold)
                {
                    List<string> keys = new List<string>(agent.gameObjectParameters.Keys);
                    foreach (string key in keys)
                    {
                        EditorGUILayout.BeginHorizontal();
                        EditorGUILayout.LabelField(key);
                        agent.gameObjectParameters[key] = EditorGUILayout.ObjectField(agent.gameObjectParameters[key], typeof(GameObject), true) as GameObject;
                        EditorGUILayout.EndHorizontal();
                    }
                }
            }

            if (agent.floatParameters != null)
            {
                floatFold = EditorGUILayout.Foldout(floatFold, "Floats");
                List<string> keys = new List<string>(agent.floatParameters.Keys);
                if (floatFold)
                {
                    foreach (string key in keys)
                    {
                        EditorGUILayout.BeginHorizontal();
                        EditorGUILayout.LabelField(key);
                        agent.floatParameters[key] = EditorGUILayout.FloatField(agent.floatParameters[key]);
                        EditorGUILayout.EndHorizontal();
                    }
                }
            }

            if (agent.integerParameters != null)
            {
                integerFold = EditorGUILayout.Foldout(integerFold, "Integers");
                List<string> keys = new List<string>(agent.integerParameters.Keys);
                if (integerFold) {
                    foreach (string key in keys)
                    {
                        EditorGUILayout.BeginHorizontal();
                        EditorGUILayout.LabelField(key);
                        agent.integerParameters[key] = EditorGUILayout.IntField(agent.integerParameters[key]);
                        EditorGUILayout.EndHorizontal();
                    }
                }
            }
            if (agent.boolParameters != null)
            {
                boolFold = EditorGUILayout.Foldout(boolFold, "Booleans");
                List<string> keys = new List<string>(agent.boolParameters.Keys);
                if (boolFold)
                {
                    foreach (string key in keys)
                    {
                        EditorGUILayout.BeginHorizontal();
                        EditorGUILayout.LabelField(key);
                        agent.boolParameters[key] = EditorGUILayout.Toggle(agent.boolParameters[key]);
                        EditorGUILayout.EndHorizontal();
                    }
                }
            }

            if (agent.vector3Parameters != null)
            {
                vectorFold = EditorGUILayout.Foldout(vectorFold, "Vectors");
                List<string> keys = new List<string>(agent.vector3Parameters.Keys);
                if (vectorFold)
                {
                    foreach (string key in keys)
                    {
                        EditorGUILayout.BeginHorizontal();
                        agent.vector3Parameters[key] = EditorGUILayout.Vector3Field(key,agent.vector3Parameters[key]);
                        EditorGUILayout.EndHorizontal();
                    }
                }
            }
        }
    }
}