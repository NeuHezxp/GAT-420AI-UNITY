using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIStateMachine
{
    private Dictionary<string, AIState> states = new Dictionary<string, AIState>(); // Stores all states in a dictionary
    public AIState CurrentState { get; private set; } = null;

    public void Update()
    {
        CurrentState?.OnUpdate();
    }

    public void AddState(string name, AIState state)
    {
        Debug.Assert(!states.ContainsKey(name), "State machine already contains state " + name); //debugging for if the state machine already contains a state

        states[name] = state; //name is the key, then goes to a node
    }

    public void SetState(string name)
    {
        Debug.Assert(states.ContainsKey(name), "State machine does not contain state " + name);

        AIState newState = states[name];

        if (newState == CurrentState) return;

        CurrentState?.OnExit();
        CurrentState = newState;
        CurrentState.OnEnter();
    }
}