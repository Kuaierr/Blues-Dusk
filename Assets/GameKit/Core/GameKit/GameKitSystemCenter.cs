using System;
using System.Collections.Generic;

namespace GameKit
{
    public static class GameKitSystemCenter
    {
        private static readonly CachedLinkedList<GameKitSystem> s_GameKitSystems = new CachedLinkedList<GameKitSystem>();
        internal const int GameKitSceneId = 0;

        public static T GetSystem<T>() where T : GameKitSystem
        {
            return (T)GetSystem(typeof(T));
        }

        public static GameKitSystem GetSystem(Type type)
        {
            LinkedListNode<GameKitSystem> current = s_GameKitSystems.First;
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

        public static GameKitSystem GetSystem(string typeName)
        {
            LinkedListNode<GameKitSystem> current = s_GameKitSystems.First;
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

        internal static void RegisterSystem(GameKitSystem GameKitSystem)
        {
            if (GameKitSystem == null)
            {
                Utility.Debugger.LogError("Game Kit System is invalid.");
                return;
            }

            Type ctype = GameKitSystem.GetType();

            LinkedListNode<GameKitSystem> current = s_GameKitSystems.First;
            while (current != null)
            {
                if (current.Value.GetType() == ctype)
                {
                    Utility.Debugger.LogError("Game Kit System type is already exist: " + ctype.FullName);
                    return;
                }
                current = current.Next;
            }

            s_GameKitSystems.AddLast(GameKitSystem);
        }
    }
}

