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

        public Dictionary<string, GameObject> gameObjectParameters = null;
        public Dictionary<string,float> floatParameters = null;
        public Dictionary<string,int> integerParameters = null;
        public Dictionary<string,bool> boolParameters = null;

        [HideInInspector]
        public List<BehaviourTreeNodeState> nodes;
        private GameObject sceneSelected;
        BehaviourTreeNode root;

        public Dictionary<Vector2, BehaviourTreeNodeState> states;

        // Use this for initialization
        void Start()
        {
            gameObjectParameters = new Dictionary<string, GameObject>();
            floatParameters = new Dictionary<string, float>();
            integerParameters = new Dictionary<string, int>();
            boolParameters = new Dictionary<string, bool>();

            foreach(string parameter in tree.gameObjectParameters)
            {
                gameObjectParameters.Add(parameter, null);
            }
            foreach (string parameter in tree.floatParameters)
            {
                floatParameters.Add(parameter, 0.0f);
            }
            foreach (string parameter in tree.integerParameters)
            {
                integerParameters.Add(parameter, 0);
            }
            foreach (string parameter in tree.boolParameters)
            {
               boolParameters.Add(parameter, false);
            }

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