using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NodeEditorFramework;

namespace AITools
{
    [Node(false, "AITools/Inverter Node")]
    public class InverterNode : DecoratorNode
    {

        public new const string ID = "inverterNode";
        public override string GetID { get { return ID; } }

        public override string Title { get { return "Inverter Node"; } }
        public override Vector2 DefaultSize { get { return new Vector2(150, 60); } }

        protected override IEnumerator process(BehaviourTreeAgent agent)
        {
            BehaviourTreeNodeState state = stateForAgent(agent);
            BehaviourTreeNode child = outputKnob.connection(0).body as BehaviourTreeNode;
            BehaviourTreeNodeState childState = child.stateForAgent(agent);
            yield return agent.StartCoroutine(child.routine(childState));
            if (childState.actualCondition == processCondition.Failure)
                state.actualCondition = processCondition.Sucess;
            if (childState.actualCondition == processCondition.Sucess)
                state.actualCondition = processCondition.Failure;
        }
    }
}
