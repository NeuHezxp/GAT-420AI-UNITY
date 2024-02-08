using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] //allows seeing in the inspector.
public class ValueRef<T>//T is generics / creates a class with a value type
{
	public T value;

	public ValueRef() { }
	public ValueRef(T value) { this.value = value; }

	public static implicit operator T(ValueRef<T> r) { return r.value; }
}
