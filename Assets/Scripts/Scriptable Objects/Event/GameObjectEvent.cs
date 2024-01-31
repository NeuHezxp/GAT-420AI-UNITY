using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Events/GameObject Event")]
public class GameObjectEvent : ScriptableObjectBase
{

	public UnityAction<GameObject> onEvenRaised;

	public void RaiseEvent(GameObject value)
	{
		onEvenRaised?.Invoke(value);
	}

	public void Subscribe(UnityAction<GameObject> function)
	{
		onEvenRaised += function;
	}

	public void Unsubscribe(UnityAction<GameObject> function)
	{
		onEvenRaised -= function;
	}
}