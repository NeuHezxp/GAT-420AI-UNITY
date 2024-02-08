using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIChaseState : AIState
{

    public AIChaseState(AIStateAgent agent) : base(agent)
    {
        //to attack
	    AIStateTransition transition = new AIStateTransition(nameof(AIAttackState));
		transition.AddCondition(new FloatCondition(agent.enemyDistance, Condition.Predicate.LESS,2));
        transition.AddCondition(new BoolCondition(agent.enemySeen));
		transitions.Add(transition);

		//to Idle
		transition = new AIStateTransition(nameof(AIIdleState));
		transition.AddCondition(new BoolCondition(agent.enemySeen, false));
		transitions.Add(transition);

		//to Patrol
		transition = new AIStateTransition(nameof(AIPatrolState));
		transition.AddCondition(new BoolCondition(agent.enemySeen, false));
		transition.AddCondition(new BoolCondition(agent.enemiesSeen, false));
		transitions.Add(transition);

	}
    float initialSpeed;
    public override void OnEnter()
    {
        agent.movement.Resume();
        initialSpeed = agent.movement.maxSpeed;
         agent.movement.maxSpeed *= 2;
    }
    public override void OnUpdate()
    {
        if(agent.enemySeen) agent.movement.Destination = agent.enemy.transform.position;
    }

    public override void OnExit()
    {
        agent.movement.maxSpeed = initialSpeed;
    }



}
