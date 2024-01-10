using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AiPerception : MonoBehaviour
{
   public string tagName = "";
   public float distance = 1;
   public float maxAngle = 45;

   public abstract GameObject[] GetGameObjects();


}
