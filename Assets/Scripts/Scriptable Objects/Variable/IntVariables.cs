using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Variables/Int")]
public class IntVariables : ScriptableObject, ISerializationCallbackReceiver
{
	public int initialValue;

	[NonSerialized]
	public int value;

	public void OnBeforeSerialize()
	{
		//
	}

	public void OnAfterDeserialize()
	{
		value = initialValue;
	}
}