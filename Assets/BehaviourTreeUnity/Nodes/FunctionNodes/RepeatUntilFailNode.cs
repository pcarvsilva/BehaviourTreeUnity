using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NodeEditorFramework;

namespace AITools
{
    [Node(false, "AITools/Repeat Until Fail Node")]
    public class RepeatUntilFailNode : DecoratorNode
    {

        public const string ID = "repeatUntilFailNode";
        public override string GetID { get { return ID; } }

        public override string Title { get { return "Repeat Until Fail Node"; } }
        public override Vector2 DefaultSize { get { return new Vector2(150, 60); } }

        public int repetitionQuantity;
        private int actualRepetition;

        public bool repeatForever = false;

        protected override IEnumerator process(BehaviourTreeAgent agent)
        {
            actualRepetition = 0;
            BehaviourTreeNodeState state = stateForAgent(agent);
            BehaviourTreeNode child = outputKnob.connection(0).body as BehaviourTreeNode;
            BehaviourTreeNodeState childState = child.stateForAgent(agent);
            do
            {
                yield return agent.StartCoroutine(child.routine(childState));
                actualRepetition++;
            } while ((repeatForever || actualRepetition < repetitionQuantity) && childState.actualCondition != processCondition.Failure);
            if(childState.actualCondition == processCondition.Failure)
                state.actualCondition = processCondition.Sucess;
            if (childState.actualCondition == processCondition.Sucess)
                state.actualCondition = processCondition.Failure;
        }
    }
}
