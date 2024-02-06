using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPatrolState : AIState
{
    Vector3 destination;

    public AIPatrolState(AIStateAgent agent) : base(agent)
    {
    }

    public override void OnEnter()
    {
      var navNode = AINavNode.GetRandomAINavNode();
        destination = navNode.transform.position;
    }

    public override void OnUpdate()
    {
        //move towards destination then idle
        agent.movement.MoveTowards(destination);
        if(Vector3.Distance(agent.transform.position,destination) < 1)
        {
            agent.stateMachine.SetState(nameof(AIIdleState));
        }
        var enemies = agent.enemyPerception.GetGameObjects();
        if (enemies.Length > 2)
        {
            agent.stateMachine.SetState(nameof(AIChaseState));
        }
        var friends = agent.friendPerception.GetGameObjects();
        if (friends.Length > 1)
        {
            Debug.Log("Idle to Dance update");
            agent.stateMachine.SetState(nameof(AIDanceState));
        }

    }
    public override void OnExit()
    {
        
    }

}