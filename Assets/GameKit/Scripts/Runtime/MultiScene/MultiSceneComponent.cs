using GameKit;
// using GameKit.Resource;
using GameKit.Scene;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UnityGameKit.Runtime
{
    [DisallowMultipleComponent]
    [AddComponentMenu("Game Kit/GameKit MultiScene Component")]
    public sealed class MultiSceneComponent : GameKitComponent
    {
        private EventComponent m_EventComponent = null;
        private readonly SortedDictionary<string, int> m_SceneOrder = new SortedDictionary<string, int>(StringComparer.Ordinal);
        private SceneCollectionSO m_CurrentCollection;

        // public void LoadSceneCollection(string collectionName, )
        // {

        // }

        private void OnLoadSceneSuccess(object sender, GameKit.Scene.LoadSceneSuccessEventArgs e)
        {
            // Log.Info("GameKit.Scene.LoadSceneSuccessEventArgs");
            if (!m_SceneOrder.ContainsKey(e.SceneAssetName))
            {
                m_SceneOrder.Add(e.SceneAssetName, 0);
            }

            m_EventComponent.Fire(this, LoadSceneSuccessEventArgs.Create(e));
        }

        private void OnLoadSceneFailure(object sender, GameKit.Scene.LoadSceneFailureEventArgs e)
        {
            m_EventComponent.Fire(this, LoadSceneFailureEventArgs.Create(e));
        }

        private void OnUnloadSceneSuccess(object sender, GameKit.Scene.UnloadSceneSuccessEventArgs e)
        {
            m_EventComponent.Fire(this, UnloadSceneSuccessEventArgs.Create(e));
        }

        private void OnUnloadSceneFailure(object sender, GameKit.Scene.UnloadSceneFailureEventArgs e)
        {
            m_EventComponent.Fire(this, UnloadSceneFailureEventArgs.Create(e));
        }
    }
}
