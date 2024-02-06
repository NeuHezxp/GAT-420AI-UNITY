using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIDanceState : AIState
{
    float timer;
    public AIDanceState(AIStateAgent agent) : base(agent)
    {
    }

    public override void OnEnter()
    {
        agent.animator?.SetTrigger("Dance");
        timer = Time.time + 2;
    }
    public override void OnUpdate()
    {
        if (Time.time > timer)
        {
            agent.stateMachine.SetState(nameof(AIPatrolState));
        }
    }

    public override void OnExit()
    {
        
    }

}
