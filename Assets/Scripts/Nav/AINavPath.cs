using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AINavPath : MonoBehaviour
{
    //[RequireComponent(typeof(AINavAgent))]
    public enum ePathType
    {
        Waypoint,
        Dijkstra,
        AStar
    }
    [SerializeField] ePathType pathType;
    [SerializeField] AINavNode startNode;
    [SerializeField] AINavNode EndNode;
    AINavAgent agent;
    public AINavNode targetNode { get; set; } = null;
    List<AINavNode> path = new List<AINavNode>();






    public Vector3 destination
    {
        get
        {
            return (targetNode != null) ? targetNode.transform.position : Vector3.zero;
        }
        set
        {
            if (pathType == ePathType.Waypoint) { targetNode = agent.GetNearestAINavNode(value); }
            else if (pathType == ePathType.Dijkstra || pathType == ePathType.AStar)
            {
                generatePath(startNode, EndNode);
            }
        }
    }

    private void Start()
    {
        agent = GetComponent<AINavAgent>();
        targetNode = (startNode != null) ? startNode : AINavNode.GetRandomAINavNode();

    }

    public bool HasTarget()
    {
        return targetNode != null;
    }

    public AINavNode GetNextAINavNode(AINavNode node)
    {
        if (pathType == ePathType.Waypoint) return node.GetRandomNeighbor();

        if (pathType == ePathType.Dijkstra || pathType == ePathType.AStar) return GetNextPathAINavNode(node);
        return null;
    }

    private void generatePath(AINavNode start, AINavNode end)
    {
        AINavNode.ResetNodes();
        AINavDijkstra.Generate(start, end, ref path);
    }
    private AINavNode GetNextPathAINavNode(AINavNode node)
    {
        if (path.Count == 0) return null;
        int index = path.FindIndex(pathNode => pathNode == node);
        if(index == -1) return null;
        if (index + 1 == path.Count) return null;

        AINavNode nextNode = path[index + 1];
        return null;
    }
}