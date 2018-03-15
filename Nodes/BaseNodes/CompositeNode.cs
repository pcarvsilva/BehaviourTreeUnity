using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NodeEditorFramework;
using NodeEditorFramework.Utilities;

namespace AITools
{

    [Node(false, "AITools/Composite Node")]
    public abstract class CompositeNode : BehaviourTreeNode
    {
        public const string ID = "compositeNode";
        public override string GetID { get { return ID; } }

        public override string Title { get { return "Composite Node"; } }

        public override Vector2 MinSize { get { return new Vector2(200, 10); } }
        public override bool AutoLayout { get { return true; } } 

        public List<string> labels = new List<string>();
        private string newLabel = "";

        [ConnectionKnob("Input", Direction.In, "Flow",ConnectionCount.Single)]
        public ConnectionKnob input;

        private ConnectionKnobAttribute dynaCreationAttribute = new ConnectionKnobAttribute("Child", Direction.Out, "Flow");

        public override void NodeGUI()
        {
            dynaCreationAttribute.MaxConnectionCount = ConnectionCount.Single;
            if (dynamicConnectionPorts.Count != labels.Count)
            { // Make sure labels and ports are synchronised
                while (dynamicConnectionPorts.Count > labels.Count)
                    DeleteConnectionPort(dynamicConnectionPorts.Count - 1);
                while (dynamicConnectionPorts.Count < labels.Count)
                    CreateConnectionKnob(dynaCreationAttribute);
            }

            input.DisplayLayout();
            GUILayout.Label("Runs child nodes in sequence");

            // Display text field and add button
            GUILayout.BeginHorizontal();
            newLabel = (labels.Count + 1).ToString() ;
            GUILayout.EndHorizontal();

            for (int i = 0; i < labels.Count; i++)
            { // Display label and delete button
                GUILayout.BeginHorizontal();
                GUILayout.Label(labels[i]);
                ((ConnectionKnob)dynamicConnectionPorts[i]).SetPosition();
                if (GUILayout.Button("x", GUILayout.ExpandWidth(false)))
                { // Remove current label
                    labels.RemoveAt(i);
                    DeleteConnectionPort(i);
                    i--;
                }
                GUILayout.EndHorizontal();
            }
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Add", GUILayout.ExpandWidth(false)))
            {
                labels.Add(newLabel);
                CreateConnectionKnob(dynaCreationAttribute);
            }
            GUILayout.EndHorizontal();
        }
    }
}