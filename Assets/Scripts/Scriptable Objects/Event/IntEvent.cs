using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/Int Event")]
public class IntEvent : ScriptableObjectBase
{

	public UnityAction<int> onEvenRaised;

	public void RaiseEvent(int value)
	{
		onEvenRaised?.Invoke(value);
	}

	public void Subscribe(UnityAction<int> function)
	{
		onEvenRaised += function;
	}

	public void Unsubscribe(UnityAction<int> function)
	{
		onEvenRaised -= function;
	}
}