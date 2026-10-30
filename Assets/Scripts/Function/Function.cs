using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName="Function", menuName="Function/Function")]
public class Function : ScriptableObject,IFunction
{
    public float value;
    public virtual void Functions(GameObject player,GameObject enemy)
    {
    }
}
