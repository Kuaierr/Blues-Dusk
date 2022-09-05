using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class PoolBase_SO : ScriptableObject 
{
    public abstract T GetData<T>(string name) where T : ScriptableObject;
    public abstract T GetData<T>(int id) where T : ScriptableObject;
}
