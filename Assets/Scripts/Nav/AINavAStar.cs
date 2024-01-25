using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Priority_Queue;

public class AINavAStar
{
    public static bool Generate(AINavNode start, AINavNode end, ref List<AINavNode> path)
    {
        var nodes = new SimplePriorityQueue<AINavNode>();

        start.Cost = 0;
        float heuristic = Vector3.Distance(start.transform.position, end.transform.position);


        nodes.EnqueueWithoutDuplicates(start, start.Cost + heuristic);

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

                    heuristic = Vector3.Distance(neighbor.transform.position, end.transform.position);
                    nodes.EnqueueWithoutDuplicates(neighbor, neighbor.Cost + heuristic);
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
