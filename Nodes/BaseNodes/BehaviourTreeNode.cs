using UnityEngine;
using NodeEditorFramework;
using System.Collections;
using System.Linq;
using UnityEditor;
using System.Collections.Generic;

namespace AITools
{
    public abstract class BehaviourTreeNode : Node
    {
        [HideInInspector]
        public int indexInTree;


        public Coroutine stopNode;

        public override bool AutoLayout { get { return true; } }
        public override Vector2 MinSize { get { return new Vector2(200, 10); } }


        protected Color BackgroundColor
        {
            get
            {
                if (BehaviourTree.selectedAgent == null)
                {
                    return Color.white;
                }
                try
                {
                    BehaviourTreeNodeState editorState = BehaviourTree.selectedAgent.states[position];
                    if (!EditorApplication.isPlaying)
                    {
                        return Color.white;
                    }
                    else if (editorState.actualCondition == processCondition.Running)
                    {
                        return Color.cyan;
                    }
                    else if (editorState.actualCondition == processCondition.Sucess)
                    {
                        return Color.green;
                    }
                    else if (editorState.actualCondition == processCondition.Failure)
                    {
                        return Color.red;
                    }
                    else
                    {
                        return Color.white;
                    }
                }
                catch
                {
                    return Color.white;
                }
            }
        }

        public override void NodeGUI()
        {
            GUILayout.BeginHorizontal();
            GUILayout.BeginVertical();

            Editor e = Editor.CreateEditor(this);
            e.DrawDefaultInspector();

            GUILayout.EndVertical();
            GUILayout.EndHorizontal();

        }

        protected internal override void DrawNode()
        {
            backgroundColor = BackgroundColor;
            base.DrawNode();
        }

        protected abstract IEnumerator process(BehaviourTreeAgent agent);

        public IEnumerator routine(BehaviourTreeNodeState parentState) {
            BehaviourTreeNodeState state = stateForAgent(parentState.agent);
            if (stopNode != null)
            {
                parentState.agent.StopCoroutine(stopNode);
            }
            state.actualCondition = processCondition.Running;
            yield return parentState.agent.StartCoroutine(process(parentState.agent));
            yield return new WaitForSeconds(0.3f);
            parentState.agent.StartCoroutine(refreshNode(state));
           
        }

        private IEnumerator refreshNode(BehaviourTreeNodeState state)
        {
            yield return new WaitForSeconds(2.0f);
            state.actualCondition = processCondition.Stopped;
            stopNode = null;
        }

        public BehaviourTreeNodeState stateForAgent(BehaviourTreeAgent agent)
        {
           BehaviourTreeNodeState state = agent.nodes.Where(x => x.node.position == this.position).First();
           return state;
        }
    }

    public enum processCondition {
        Sucess,
        Failure,
        Running,
        Stopped
    }
}