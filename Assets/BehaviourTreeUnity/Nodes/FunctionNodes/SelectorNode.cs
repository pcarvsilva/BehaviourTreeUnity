using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NodeEditorFramework;

namespace AITools
{

    [Node(false, "AITools/Selector Node")]
    public class SelectorNode : CompositeNode
    {
        public new const string ID = "Selector Node";
        public override string GetID { get { return ID; } }

        public override string Title { get { return "Selector Node"; } }

        protected override IEnumerator process(BehaviourTreeAgent agent)
        {
            BehaviourTreeNodeState myState = stateForAgent(agent);
            foreach (ConnectionPort knob in outputPorts)
            {
                if (knob.connected())
                {
                    BehaviourTreeNode node = knob.connection(0).body as BehaviourTreeNode;
                    BehaviourTreeNodeState childState = node.stateForAgent(agent);
                    yield return agent.StartCoroutine(node.routine(childState));
                    if (childState.actualCondition == processCondition.Sucess)
                    {
                        myState.actualCondition = processCondition.Sucess;
                    }
                    if (myState.actualCondition == processCondition.Sucess)
                        yield break;
                }
            }
            if (myState.actualCondition == processCondition.Running)
                myState.actualCondition = processCondition.Failure;
        }
    }
}