using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace UnityGameKit.Runtime
{
    [CreateAssetMenu(fileName = "SceneSO", menuName = "GameKit/SceneSO", order = 0)]
    public class SceneSO : ScriptableObject
    {
        public SceneType SceneType;
        public string SceneName;
    }
}

