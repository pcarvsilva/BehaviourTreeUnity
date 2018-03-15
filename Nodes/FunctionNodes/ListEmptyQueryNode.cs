using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AITools;
using UnityEngine.AI;
using NodeEditorFramework;

[Node(false, "AITools/List Empty Query Node")]
public class ListEmptyQueryNode : LeafNode
{

    public string listToCheck;
    public new const string ID = "ListEmptyNode";
    public override string GetID { get { return ID; } }

    public override string Title { get { return "List Empty Node"; } }
    public override Vector2 DefaultSize { get { return new Vector2(150, 60); } }

    protected override IEnumerator process(BehaviourTreeAgent agent)
    {
        BehaviourTreeNodeState state = stateForAgent(agent);
        if (agent.listParameters[listToCheck].Count == 0)
        {
            state.actualCondition = processCondition.Sucess;
        }
        else
        {
            state.actualCondition = processCondition.Failure;
        }
        yield return null;
    }

}
