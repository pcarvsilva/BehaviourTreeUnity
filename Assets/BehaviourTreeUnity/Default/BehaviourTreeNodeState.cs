using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AITools
{

    public class BehaviourTreeNodeState
    {
        public processCondition actualCondition;
        public BehaviourTreeNode node;
        public BehaviourTreeAgent agent;
    }

}