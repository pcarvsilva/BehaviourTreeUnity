using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NodeEditorFramework;
using UnityEditor;

namespace AITools
{

    [Node(false, "AITools/Leaf Node")]
    public abstract class LeafNode : BehaviourTreeNode
    {
        public const string ID = "leafNode";
        public override string GetID { get { return ID; } }

        public override string Title { get { return "Leaf Node"; } }
        public override Vector2 DefaultSize { get { return new Vector2(150, 60); } }

        [HideInInspector]
        [ConnectionKnob("Input", Direction.In, "Flow")]
        public ConnectionKnob inputKnob;


        public override void NodeGUI()
        {
            GUILayout.Label("Leaf Node");

            GUILayout.BeginHorizontal();
            GUILayout.BeginVertical();

            inputKnob.DisplayLayout();

            GUILayout.EndVertical();
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.BeginVertical();

            Editor.CreateEditor(this).DrawDefaultInspector();


            GUILayout.EndVertical();
            GUILayout.EndHorizontal();

        }

    }
}