using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Priority_Queue;

public class AINavDijkstra
{
    public static bool Generate(AINavNode start, AINavNode end, ref List<AINavNode> path)
    {
        var nodes = new SimplePriorityQueue<AINavNode>();

        start.Cost = 0;
        nodes.EnqueueWithoutDuplicates(start, start.Cost);

        bool found = false;
        while (!found && nodes.Count > 0)
        {
            var node = nodes.Dequeue();

            if (node == end)
            {
                found = true;
                break;
            }
            foreach (var neighbor in node.neighbors)
            {
                float cost = node.Cost + Vector3.Distance(node.transform.position, neighbor.transform.position);
                if (cost < neighbor.Cost)
                {
                    neighbor.Cost = cost;
                    neighbor.Parent = node;
                    nodes.EnqueueWithoutDuplicates(neighbor, cost);
                }
            }
        }
        path.Clear();
        if (found)
        {
            CreatePathFromParents(end, ref path);
        }

        return false;
    }
    public static void CreatePathFromParents(AINavNode node, ref List<AINavNode> path) //goes through and finds their parents and reverses
    {
        // while node not null
        while (node != null)
        {
            // add node to list path
            path.Add(node);
            // set node to node parent
            node = node.Parent;
        }

        // reverse path
        path.Reverse();
    }
}
