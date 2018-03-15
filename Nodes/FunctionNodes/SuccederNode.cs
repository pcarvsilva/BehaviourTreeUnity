using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NodeEditorFramework;

namespace AITools
{
    [Node(false, "AITools/Succeder Node")]
    public class SuccederNode : DecoratorNode
    {

        public new const string ID = "succederNode";
        public override string GetID { get { return ID; } }

        public override string Title { get { return "Succeder Node"; } }
        public override Vector2 DefaultSize { get { return new Vector2(150, 60); } }

        protected override IEnumerator process(BehaviourTreeAgent agent)
        {
            BehaviourTreeNodeState state = stateForAgent(agent);
            BehaviourTreeNode child = outputKnob.connection(0).body as BehaviourTreeNode;
            BehaviourTreeNodeState childState = child.stateForAgent(agent);
            yield return agent.StartCoroutine(child.routine(childState));
            state.actualCondition = processCondition.Sucess;
        }
    }
}
