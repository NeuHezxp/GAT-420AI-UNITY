using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAttackState : AIState
{
    
    public AIAttackState(AIStateAgent agent) : base(agent)
    {
		AIStateTransition transition = new AIStateTransition(nameof(AIPatrolState)); // <
		transition.AddCondition(new FloatCondition(agent.timer, Condition.Predicate.LESS, 0));
		transitions.Add(transition);

        transition = new AIStateTransition(nameof(AIFleeState));
        transition.AddCondition(new FloatCondition(agent.timer, Condition.Predicate.LESS, 0));
        transition.AddCondition(new BoolCondition(agent.enemiesSeen));
	}

    public override void OnEnter()
    {
        agent.movement.Stop();
        agent.movement.Velocity = Vector3.zero;
        agent.animator?.SetTrigger("Attack");

        agent.timer.value = 2; //transitions after 2 seconds

        
    }
    public override void OnUpdate()
    {
        
    }

    public override void OnExit()
    {
        
    }

}
