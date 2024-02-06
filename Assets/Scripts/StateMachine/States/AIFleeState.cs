using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AIFleeState : AIState
{
    float initialSpeed;
    Vector3 fleeDirection;
    float safeDistance = 5f; // Distance at which the agent considers itself safe and stops fleeing

    float timer;
    public AIFleeState(AIStateAgent agent) : base(agent)
    {

    }

    public override void OnEnter()
    {
        timer = Time.time + 2;

        // Save the initial speed to restore it later
        initialSpeed = agent.movement.maxSpeed;
        // Increase speed to flee faster
        agent.movement.maxSpeed *= 1.5f; // Increase the speed by 50% when fleeing

        // Determine flee direction
        CalculateFleeDirection();
    }
    public override void OnUpdate()
    {
        var enemies = agent.enemyPerception.GetGameObjects();
        if (Time.time > timer)
        {
            agent.stateMachine.SetState(nameof(AIIdleState));
        }
        if (enemies.Length <= 2 || Vector3.Distance(agent.transform.position, AINavNode.GetRandomAINavNode().transform.position) < safeDistance)
        {
            // Consider the agent safe if enemies are further than safeDistance
            agent.stateMachine.SetState(nameof(AIIdleState));
        }
        else
        {
            // Continue fleeing
            agent.movement.MoveTowards(fleeDirection); // Assuming there's a Move method in your agent's movement component
        }

    }

    public override void OnExit()
    {
        agent.movement.maxSpeed = initialSpeed;
    }


    void CalculateFleeDirection()
    {
        var enemies = agent.enemyPerception.GetGameObjects();
        Vector3 averageEnemyPosition = Vector3.zero;
        foreach (var enemy in enemies)
        {
            averageEnemyPosition += enemy.transform.position;
        }
        if (enemies.Length > 0)
        {
            averageEnemyPosition /= enemies.Length;
            fleeDirection = (agent.transform.position - averageEnemyPosition).normalized;
        }
        else
        {
            // Default flee direction is opposite to agent's forward if no enemies detected
            fleeDirection = -agent.transform.forward;
        }
    }


}
