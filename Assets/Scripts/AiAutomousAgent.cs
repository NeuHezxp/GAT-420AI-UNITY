using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIAutonomousAgent : AIAgent
{
    public AIPerception seekPerception = null;
    public AIPerception fleePerception = null;
    public AIPerception flockPerception = null;

    private Vector3 WanderTarget;

    private void Start()
    {
        WanderTarget = Vector3.zero;
    }
    private void Update()
    {
        // seek
        if (seekPerception != null)
        {
            var gameObjects = seekPerception.GetGameObjects();
            if (gameObjects.Length > 0)
            {
                Vector3 force = Seek(gameObjects[0]);
                movement.ApplyForce(force);

                Debug.Log("Seeking Target: " + seekPerception.GetGameObjects()[0].name);
               
            }
        }

        // flee
        if (fleePerception != null)
        {
            var gameObjects = fleePerception.GetGameObjects();
            if (gameObjects.Length > 0)
            {
                Vector3 force = Flee(gameObjects[0]);
                movement.ApplyForce(force);

                Debug.Log("fleeing Target: " + fleePerception.GetGameObjects()[0].name);
                
            }
        }

        // flock
        if (flockPerception != null)
        {
            var gameObjects = flockPerception.GetGameObjects();
            if (gameObjects.Length > 0)
            {
                movement.ApplyForce(Cohesion(gameObjects));
                movement.ApplyForce(Separation(gameObjects, 3));
                movement.ApplyForce(Alignment(gameObjects));

                Debug.Log("flocking: " + flockPerception.GetGameObjects()[0].name);
                
            }
        }

        // If no other behaviors are active, use wander
        if (seekPerception == null && fleePerception == null && flockPerception == null)
        {
            Vector3 force = Wander();
            movement.ApplyForce(force);

            Debug.Log("Wandering");
            Debug.DrawRay(transform.position, force, Color.yellow);
        }

        transform.position = Utilities.Wrap(transform.position, new Vector3(-10, -10, -10), new Vector3(10, 10, 10));
    }

    private Vector3 Seek(GameObject target)
    {
        Vector3 direction = target.transform.position - transform.position;
        Vector3 force = GetSteeringForce(direction);

        return force;
    }

    private Vector3 Flee(GameObject target)
    {
        Vector3 direction = transform.position - target.transform.position;
        Vector3 force = GetSteeringForce(direction);

        return force;
    }

    private Vector3 Cohesion(GameObject[] neighbors)
    {
        Vector3 positions = Vector3.zero;
        foreach (var neighbor in neighbors)
        {
            positions += neighbor.transform.position;
        }

        Vector3 center = positions / neighbors.Length;
        Vector3 direction = center - transform.position;

        Vector3 force = GetSteeringForce(direction);

        return force;
    }

    private Vector3 Separation(GameObject[] neighbors, float radius)
    {
        Vector3 separation = Vector3.zero;
        foreach (var neighbor in neighbors)
        {
            // get direction vector away from neighbor
            Vector3 direction = (transform.position - neighbor.transform.position);
            // check if within separation radius
            if (direction.magnitude < radius)
            {
                // scale separation vector inversely proportional to the direction distance
                // closer the distance the stronger the separation
                separation += direction / direction.sqrMagnitude;
            }
        }

        Vector3 force = GetSteeringForce(separation);

        return force;
    }

    private Vector3 Alignment(GameObject[] neighbors)
    {
        Vector3 velocities = Vector3.zero;
        foreach (var neighbor in neighbors)
        {
            velocities += neighbor.GetComponent<AIAgent>().movement.Velocity;
        }

        Vector3 averageVelocity = velocities / neighbors.Length;

        Vector3 force = GetSteeringForce(averageVelocity);

        return force;
    }
    private Vector3 Wander()
    {
        // Adjust the WanderTarget position randomly
        // You might need to tweak these values based on your specific needs
        float wanderRadius = 5.0f;
        float wanderDistance = 3.0f;
        float wanderJitter = 1.0f;

        WanderTarget += new Vector3(Random.Range(-1f, 1f) * wanderJitter, 0, Random.Range(-1f, 1f) * wanderJitter);
        WanderTarget.Normalize();
        WanderTarget *= wanderRadius;

        Vector3 targetLocal = WanderTarget + new Vector3(0, 0, wanderDistance);
        Vector3 targetWorld = transform.TransformPoint(targetLocal);

        Vector3 wanderForce = targetWorld - transform.position;

        return wanderForce.normalized * movement.maxSpeed - movement.Velocity;
    }

    private Vector3 GetSteeringForce(Vector3 direction)
    {
        Vector3 desired = direction.normalized * movement.maxSpeed;
        Vector3 steer = desired - movement.Velocity;
        Vector3 force = Vector3.ClampMagnitude(steer, movement.maxForce);

        return force;
    }
}