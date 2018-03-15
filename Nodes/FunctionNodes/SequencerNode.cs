using System.Collections;
using UnityEngine;
using NodeEditorFramework;

namespace AITools
{
    [Node(false, "AITools/Sequencer Node")]
    public class SequencerNode : CompositeNode
    {
        public new const string ID = "Sequencer Node";
        public override string GetID { get { return ID; } }

        public override string Title { get { return "Sequencer Node"; } }

        protected override IEnumerator process(BehaviourTreeAgent agent)
        {
            BehaviourTreeNodeState myState = stateForAgent(agent);
            foreach (ConnectionPort knob in outputPorts)
            {
                if (knob.connected())
                {
                    BehaviourTreeNode node = knob.connection(0).body as BehaviourTreeNode;
                    BehaviourTreeNodeState childState = node.stateForAgent(agent);
                    yield return agent.StartCoroutine( node.routine(childState));
                    if (childState.actualCondition != processCondition.Sucess)
                    {
                        myState.actualCondition = processCondition.Failure;
                        break;
                    }
                }
            }
            if (myState.actualCondition == processCondition.Running)
                myState.actualCondition = processCondition.Sucess;
        }
    }
}