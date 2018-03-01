using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NodeEditorFramework;
using NodeEditorFramework.Utilities;
using UnityEditor;

namespace AITools
{

    [Node(false, "AITools/Decorator Node")]
    public abstract class DecoratorNode : BehaviourTreeNode
    {
        public const string ID = "decoratorNode";
        public override string GetID { get { return ID; } }

        public override string Title { get { return "Decorator Node"; } }
        public override Vector2 DefaultSize { get { return new Vector2(150, 60); } }

        [ConnectionKnob("Input", Direction.In, "Flow")]
        public ConnectionKnob inputKnob;
        [ConnectionKnob("Output", Direction.Out, "Flow")]
        public ConnectionKnob outputKnob;

        public override void NodeGUI()
        {
            GUILayout.Label("Decorator Node");

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