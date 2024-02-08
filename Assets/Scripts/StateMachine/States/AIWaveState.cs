using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIWaveState : AIState
{
    float timer;
    public AIWaveState(AIStateAgent agent) : base(agent)
    {
		AIStateTransition transition = new AIStateTransition(nameof(AIPatrolState)); // <
		transition.AddCondition(new FloatCondition(agent.timer, Condition.Predicate.LESS, 0));
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
       agent.animator?.SetTrigger("Waving");
		Debug.Log("wave enter");
		agent.timer.value = Random.Range(1, 4);

		agent.waveCooldown.value = agent.timer.value + 2; // 2 seconds after waving ends
	}


    public override void OnUpdate()
    {
        
    }
    public override void OnExit()
    {
        
    }
}
