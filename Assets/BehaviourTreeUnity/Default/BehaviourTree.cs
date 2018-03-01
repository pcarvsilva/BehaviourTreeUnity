using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NodeEditorFramework;
using NodeEditorFramework.Standard;
using UnityEditor;

namespace AITools
{
    [NodeCanvasType("Behaviour Tree")]
    public class BehaviourTree: NodeCanvas
    {
        public static BehaviourTreeAgent selectedAgent;

        public override string canvasName { get { return "Behaviour Tree"; } }
        public bool refreshCache = false;

        protected override void OnCreate()
        {
            Traversal = new CanvasCalculator(this);
        }

        protected override void ValidateSelf()
        {
            if (Traversal == null)
                Traversal = new CanvasCalculator(this);
        }

    }
}