using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NodeEditorFramework;

namespace AITools
{
    [Node(false, "AITools/Wait Seconds Node")]
    public class WaitSecondsNode : LeafNode
    {


        public new const string ID = "Wait Seconds Node";
        public override string GetID { get { return ID; } }

        public override string Title { get { return "Wait Seconds Node"; } }

        public float waitTime;


        protected override IEnumerator process(BehaviourTreeAgent agent)
        {
            BehaviourTreeNodeState state = stateForAgent(agent);
            state.actualCondition = processCondition.Running;
            yield return new WaitForSeconds(waitTime);
            state.actualCondition = processCondition.Sucess;
        }
    }
}