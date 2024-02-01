using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIDeathState : AIState
{

    float timer = 0;

    public AIDeathState(AIStateAgent agent) : base(agent)
    {
    }

    public override void OnEnter()
    {
        agent.animator?.SetTrigger("Death"); //sets trigger from animator to death animation.
        timer = Time.time + 2; //a time 2 seconds later
    }
    public override void OnUpdate()
    {
        if (Time.time > timer)
        {
           Object.Destroy(agent.gameObject);
        }
    }

    public override void OnExit()
    {
        throw new System.NotImplementedException();
    }

}