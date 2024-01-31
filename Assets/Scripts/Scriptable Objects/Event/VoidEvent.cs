using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/Event")]
public class VoidEvent : ScriptableObjectBase
{
	public UnityAction onEvenRaised;

	public void RaiseEvent()
	{
		onEvenRaised?.Invoke();
	}

	public void Subscribe(UnityAction function)
	{
		onEvenRaised += function;
	}

	public void Unsubscribe(UnityAction function)
	{
		onEvenRaised -= function;
	}
}