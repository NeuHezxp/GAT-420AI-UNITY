using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIPatrolState : AIState
{
    Vector3 destination;

    public AIPatrolState(AIStateAgent agent) : base(agent)
    {
		AIStateTransition transition = new AIStateTransition(nameof(AIIdleState)); // <
		transition.AddCondition(new FloatCondition(agent.destinationDistance, Condition.Predicate.LESS, 1));
		transitions.Add(transition);

	    transition = new AIStateTransition(nameof(AIChaseState)); // <
		transition.AddCondition(new BoolCondition(agent.enemySeen));
        transition.AddCondition(new BoolCondition(agent.enemiesSeen, false));
		transitions.Add(transition);

		transition = new AIStateTransition(nameof(AIWaveState)); // <
		transition.AddCondition(new BoolCondition(agent.friendSeen));
		transition.AddCondition(new FloatCondition(agent.timer, Condition.Predicate.LESS, 0));
		transitions.Add(transition);

		//to flee
		transition = new AIStateTransition(nameof(AIFleeState));
		transition.AddCondition(new BoolCondition(agent.enemiesSeen));
		transitions.Add(transition);




	}

    public override void OnEnter()
    {
        agent.movement.Resume();
        var navNode = AINavNode.GetRandomAINavNode();
        destination = navNode.transform.position;
		agent.timer.value = Random.Range(1, 4);
	}

    public override void OnUpdate()
    {

        //move towards destination then idle
        agent.movement.MoveTowards(destination);


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