using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class AIIdleState : AIState
{

	float timer;


	public AIIdleState(AIStateAgent agent) : base(agent)
	{
		AIStateTransition transition = new AIStateTransition(nameof(AIPatrolState)); // <
		transition.AddCondition(new FloatCondition(agent.timer, Condition.Predicate.LESS, 0));
		transitions.Add(transition);


		transition = new AIStateTransition(nameof(AIChaseState));
		transition.AddCondition(new BoolCondition(agent.enemySeen));
		transition.AddCondition(new FloatCondition(agent.timer, Condition.Predicate.LESS, 0));
		transitions.Add(transition);

		transition = new AIStateTransition(nameof(AIWaveState)); // <
		transition.AddCondition(new BoolCondition(agent.friendSeen));
		transitions.Add(transition);

		transition = new AIStateTransition(nameof(AIDanceState)); // <
		transition.AddCondition(new BoolCondition(agent.friendsSeen));
		transition.AddCondition(new FloatCondition(agent.friendDistance, Condition.Predicate.LESS, 2));
		transition.AddCondition(new FloatCondition(agent.timer, Condition.Predicate.LESS, 0));
		transitions.Add(transition);

	}

	public override void OnEnter()
	{
		agent.movement.Stop();
		agent.movement.Velocity = Vector3.zero;
		Debug.Log("idle enter");
		agent.timer.value = Random.Range(1, 4);
	}
	public override void OnUpdate()
	{
		

		//	if (enemies.Length > 2)
		//{
		//	Debug.Log("idle to Flee");
		//	agent.stateMachine.SetState(nameof(AIFleeState));
		//}

		var friends = agent.friendPerception.GetGameObjects();
		
		if (friends.Length > 1)
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
