using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace GameKit
{
    public class QuickMonoManager : SingletonBase<QuickMonoManager>
    {
        public QuickMono controller;
        public Dictionary<string, GameObject> globalObjects;
        public QuickMonoManager()
        {
            GameObject obj = new GameObject("QuickMono");
            controller = obj.AddComponent<QuickMono>();
        }

        public void AddUpdateListener(UnityAction func)
        {
            controller?.AddUpdateListener(func);
        }

        public void RemoveUpdateListener(UnityAction func)
        {
            controller?.RemoveUpdateListener(func);
        }

        public Coroutine StartCoroutine(IEnumerator routine)
        {
            if (controller == null)
                return null;
            return controller?.StartCoroutine(routine);
        }

        public void StopCorroutie(Coroutine routine)
        {
            controller?.StopCoroutine(routine);
        }

        public Coroutine StartCoroutine(string methodName)
        {
            return controller?.StartCoroutine(methodName);
        }

    }
}
