using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class AIIdleState : AIState
{

    float timer;
    public AIIdleState(AIStateAgent agent) : base(agent)
    {
    }

    public override void OnEnter()
    {
        Debug.Log("idle enter");
        timer = Time.time + Random.Range(1,2);
    }
    public override void OnUpdate()
    {
        if (Time.time > timer)
        {
            agent.stateMachine.SetState(nameof(AIPatrolState));
        }

        var enemies = agent.enemyPerception.GetGameObjects();
        if (enemies.Length > 0)
        {
        Debug.Log("idle to Chase");
            agent.stateMachine.SetState(nameof(AIChaseState));
        }

        if (enemies.Length > 2)
        {
            Debug.Log("idle to Flee");
            agent.stateMachine.SetState(nameof(AIFleeState));
        }

        var friends = agent.friendPerception.GetGameObjects();
        if (friends.Length > 0)
        {
            Debug.Log("Idle to wave update");
            agent.stateMachine.SetState(nameof(AIWaveState));
        }
        
        if (friends.Length > 1 )
        {
            Debug.Log("Idle to Dance update");
            agent.stateMachine.SetState(nameof(AIDanceState));
        }

    }

    public override void OnExit()
    {
        Debug.Log("idle exit");
    }

}
