using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NodeEditorFramework;
using NodeEditorFramework.Standard;

namespace AITools
{
    [NodeCanvasType("Behaviour Tree")]
    public class BehaviourTree: NodeCanvas
    {
        public static BehaviourTreeAgent selectedAgent;

        public override string canvasName { get { return "Behaviour Tree"; } }

        public List<string> gameObjectParameters;
        public List<string> floatParameters;
        public List<string> integerParameters;
        public List<string> boolParameters;
        public List<string> vector3Parameters;
        public List<string> listParameters;

        protected override void OnCreate()
        {
            Traversal = new CanvasCalculator(this);
        }

        protected override void ValidateSelf()
        {
            if (Traversal == null)
                Traversal = new CanvasCalculator(this);
        }

        public enum variableType
        {
            Float,
            Integer,
            Vector3,
            Bool,
            GameObject
        }

    }
}