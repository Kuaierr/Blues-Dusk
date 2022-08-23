using System;
using System.Collections.Generic;
using UnityEngine;
using GameKit;
using GameKit.DataStructure;

namespace UnityGameKit.Runtime
{
    public enum ShutdownType
    {
        None,
        Restart,
        Quit
    }

    public static class GameKitComponentCenter
    {
        private static readonly CachedLinkedList<GameKitComponent> s_GameKitComponents = new CachedLinkedList<GameKitComponent>();
        internal const int GameKitSceneId = 0;

        public static T GetComponent<T>() where T : GameKitComponent
        {
            return (T)GetComponent(typeof(T));
        }

        public static GameKitComponent GetComponent(Type type)
        {
            LinkedListNode<GameKitComponent> current = s_GameKitComponents.First;
            while (current != null)
            {
                if (current.Value.GetType() == type)
                {
                    return current.Value;
                }

                current = current.Next;
            }
            return null;
        }

        public static GameKitComponent GetComponent(string typeName)
        {
            LinkedListNode<GameKitComponent> current = s_GameKitComponents.First;
            while (current != null)
            {
                Type type = current.Value.GetType();
                if (type.FullName == typeName || type.Name == typeName)
                {
                    return current.Value;
                }

                current = current.Next;
            }

            return null;
        }

        internal static void RegisterComponent(GameKitComponent gameKitComponent)
        {
            if (gameKitComponent == null)
            {
                Utility.Debugger.LogError("Game Kit component is invalid.");
                return;
            }

            Type ctype = gameKitComponent.GetType();

            LinkedListNode<GameKitComponent> current = s_GameKitComponents.First;
            while (current != null)
            {
                if (current.Value.GetType() == ctype)
                {
                    Utility.Debugger.LogError("Game Kit component type is already exist: " + ctype.FullName);
                    return;
                }
                current = current.Next;
            }

            s_GameKitComponents.AddLast(gameKitComponent);
        }



        public static void Shutdown(ShutdownType shutdownType)
        {
            Utility.Debugger.Log("Shutdown GameKit ({0})...", shutdownType.ToString());
            GameKitCoreComponent coreComponent = GetComponent<GameKitCoreComponent>();
            if (coreComponent != null)
            {
                coreComponent.Shutdown();
                coreComponent = null;
            }

            s_GameKitComponents.Clear();

            if (shutdownType == ShutdownType.None)
            {
                return;
            }

            if (shutdownType == ShutdownType.Restart)
            {
                // SceneManager.LoadScene(GameFrameworkSceneId);
                return;
            }

            if (shutdownType == ShutdownType.Quit)
            {
                Application.Quit();
#if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
#endif
                return;
            }
        }
    }



}

