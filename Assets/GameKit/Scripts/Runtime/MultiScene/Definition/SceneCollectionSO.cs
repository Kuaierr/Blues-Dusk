using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using GameKit;
namespace UnityGameKit.Runtime
{

    [CreateAssetMenu(fileName = "SceneCollection", menuName = "GameKit/SceneCollection", order = 0)]
    public class SceneCollectionSO : ScriptableObject
    {
        public List<SceneSO> collections;
    }
}