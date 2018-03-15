using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NodeEditorFramework;
using AITools;

[Node(false, "AITools/List Copy Node")]
public class ListCopyNode : LeafNode
{
    public string listToInsert;
    public string listToCopy;
    public new const string ID = "ListCopyNode";
    public override string GetID { get { return ID; } }

    public override string Title { get { return "List Copy Node"; } }
    public override Vector2 DefaultSize { get { return new Vector2(150, 60); } }

    protected override IEnumerator process(BehaviourTreeAgent agent)
    {
        yield return null;
        agent.listParameters[listToInsert] = new List<object>(agent.listParameters[listToCopy]);
        stateForAgent(agent).actualCondition = processCondition.Sucess;

    }

}