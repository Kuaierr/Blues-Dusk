using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using GameKit;
using GameKit.DataStructure;
public class ReferencePoolDemo : MonoBehaviour
{
    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    private void Start()
    {
        StartCoroutine(AcquirePerSec(1));
        Debug.Log("12345".Split('/').Length);
    }

    IEnumerator AcquirePerSec(float time)
    {
        Debug.Log("Acquire Object");
        ReferencePool.Acquire<Command>();
        yield return new WaitForSeconds(time);
        ReferencePool.Acquire<Command>();
        Debug.Log(ReferencePool.Count);
    }
}