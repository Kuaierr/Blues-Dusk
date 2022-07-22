using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GameKit;
using GameKit.DataStructure;
public class Demo_ReferencePool : MonoBehaviour
{
    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    
    public class CustomClass : IReference
    {
        string name;
        public CustomClass()
        {

        }

        public void Clear()
        {
            
        }
    }
    private void Start()
    {
        StartCoroutine(AcquirePerSec(1));
        Debug.Log("12345".Split('/').Length);
    }

    IEnumerator AcquirePerSec(float time)
    {
        Debug.Log("Acquire Object");
        ReferencePool.Acquire<CustomClass>();
        yield return new WaitForSeconds(time);
        ReferencePool.Acquire<CustomClass>();
        Debug.Log(ReferencePool.Count);
    }
}