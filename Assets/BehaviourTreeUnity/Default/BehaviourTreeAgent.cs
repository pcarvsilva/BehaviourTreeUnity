using System.Collections.Generic;
using UnityEngine;
using NodeEditorFramework;
using System.Linq;
using UnityEditor;


namespace AITools
{

    public class BehaviourTreeAgent : MonoBehaviour
    {
        public BehaviourTree tree;
        Dictionary<string,object> parameters;
        [HideInInspector]
        public List<BehaviourTreeNodeState> nodes;
        private GameObject sceneSelected;
        BehaviourTreeNode root;

        public Dictionary<Vector2, BehaviourTreeNodeState> states;

        // Use this for initialization
        void Start()
        {
            
            nodes = new List<BehaviourTreeNodeState>();
            states = new Dictionary<Vector2, BehaviourTreeNodeState>();
            if(tree == null)
            {
                throw new MissingReferenceException(name + " has no Behaviour Tree");
            }
            foreach(Node node in tree.nodes)
            {
                ConnectionPortManager.UpdateConnectionPorts(node);
                BehaviourTreeNodeState state = new BehaviourTreeNodeState
                {
                    agent = this,
                    node = node as BehaviourTreeNode,
                    actualCondition = processCondition.Stopped
                };
                nodes.Add(state);
                states.Add(node.position,state);
                if (node.isInput())
                {
                    root = node as BehaviourTreeNode;
                }
            }
            BehaviourTreeNodeState rootState = nodes.Where(x => x.node == root).First();
            StartCoroutine(root.routine(rootState));
        }

        void Update()
        {
            if (sceneSelected != Selection.activeGameObject)
            {
                sceneSelected = Selection.activeGameObject;
                if (sceneSelected == gameObject)
                {
                    BehaviourTree.selectedAgent = this;
                }
            }

        }
    }
}