using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NodeEditorFramework;
using NodeEditorFramework.Standard;
using AITools;

[Node(false, "AITools/List Pop Node")]
public class ListPopNode : LeafNode {

    public string listToCheck;
    public string variableToInsert;
    public BehaviourTree.variableType type;
    public new const string ID = "ListPopNode";
    public override string GetID { get { return ID; } }

    public override string Title { get { return "List Pop Node"; } }
    public override Vector2 DefaultSize { get { return new Vector2(150, 60); } }

    protected override IEnumerator process(BehaviourTreeAgent agent)
    {
        yield return null;
        switch (type)
        {
            case BehaviourTree.variableType.Float:
                agent.floatParameters[variableToInsert] = (float)agent.listParameters[listToCheck][0];
                break;
            case BehaviourTree.variableType.Vector3:
                agent.vector3Parameters[variableToInsert] = (Vector3)agent.listParameters[listToCheck][0];
                break;
            case BehaviourTree.variableType.Integer:
                agent.integerParameters[variableToInsert] = (int)agent.listParameters[listToCheck][0];
                break;
            case BehaviourTree.variableType.GameObject:
                agent.gameObjectParameters[variableToInsert] = (GameObject)agent.listParameters[listToCheck][0];
                break;
            case BehaviourTree.variableType.Bool:
                agent.boolParameters[variableToInsert] = (bool)agent.listParameters[listToCheck][0];
                break;
        }
        agent.listParameters[listToCheck].RemoveAt(0);
        stateForAgent(agent).actualCondition = processCondition.Sucess;

    }
}
