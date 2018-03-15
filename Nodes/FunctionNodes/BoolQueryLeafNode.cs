using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AITools;
using UnityEngine.AI;
using NodeEditorFramework;

[Node(false, "AITools/Bool Query Node")]
public class BoolQueryLeafNode : LeafNode
{

    public string inspectedBool;
    public new const string ID = "boolQueryNode";
    public override string GetID { get { return ID; } }

    public override string Title { get { return "Query Bool Node"; } }
    public override Vector2 DefaultSize { get { return new Vector2(150, 60); } }

    protected override IEnumerator process(BehaviourTreeAgent agent)
    {
        yield return null;
        BehaviourTreeNodeState state = stateForAgent(agent);
        bool query = agent.boolParameters[inspectedBool];

        if (query)
            state.actualCondition = processCondition.Sucess;
        else
            state.actualCondition = processCondition.Failure;
    }

}
