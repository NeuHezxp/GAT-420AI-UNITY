using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIWaveState : AIState
{
    float timer;
    public AIWaveState(AIStateAgent agent) : base(agent)
    {
    }

    public override void OnEnter()
    {
        Debug.Log("Entered Waving State");
        agent.animator?.SetTrigger("Waving");
        timer = Time.time + 2;
    }


    public override void OnUpdate()
    {
        Debug.Log("Exiting Waving State");
        if (Time.time > timer)
        {
            agent.stateMachine.SetState(nameof(AIPatrolState));
        }
    }
    public override void OnExit()
    {
        
    }
}
