using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiAutomousAgent : AiAgent
{
    public AiPerception perception = null;
    private void Update()
    {
        var gameObjects = perception.GetGameObjects();
        foreach (var go in gameObjects) //go is a gameobject
        {
            Debug.DrawLine(transform.position, go.transform.position,Color.red); //transform position coming from owners position
            Debug.Log(go.name);
        }
        //
    }

}
