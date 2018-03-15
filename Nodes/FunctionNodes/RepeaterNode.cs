using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NodeEditorFramework;

namespace AITools
{
    [Node(false, "AITools/Repeater Node")]
    public class RepeaterNode : DecoratorNode
    {

        public new const string ID = "repeaterNode";
        public override string GetID { get { return ID; } }

        public override string Title { get { return "Repeater Node"; } }
        public override Vector2 DefaultSize { get { return new Vector2(150, 60); } }

        public int repetitionQuantity;
        private int actualRepetition =0;

        public bool repeatForever = false;

        protected override IEnumerator process(BehaviourTreeAgent agent)
        {
            actualRepetition = 0;
            BehaviourTreeNodeState state = stateForAgent(agent);
            BehaviourTreeNode child = outputKnob.connection(0).body as BehaviourTreeNode;
            BehaviourTreeNodeState childState = child.stateForAgent(agent);
            while (repeatForever || actualRepetition < repetitionQuantity)
            {
                yield return agent.StartCoroutine(child.routine(childState));
                actualRepetition++;
            }            
            state.actualCondition = childState.actualCondition;
        }
    }
}
