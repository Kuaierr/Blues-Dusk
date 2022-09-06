using System.Text;
using GameKit;
using GameKit.Event;
using System.Collections.Generic;

namespace UnityGameKit.Runtime
{
    public sealed class DoTransitionCompleteEventArgs : GameEventArgs
    {
        public static readonly int EventId = typeof(DoTransitionCompleteEventArgs).GetHashCode();

        public DoTransitionCompleteEventArgs()
        {
            TransitionType = SceneTransitionType.Immediately;
            TargetNames = new List<string>();
            RemoveNames = new List<string>();
            UserData = null;
        }

        public override int Id
        {
            get
            {
                return EventId;
            }
        }

        public override bool IsManuallyRelease
        {
            get
            {
                return true;
            }
        }

        public SceneTransitionType TransitionType
        {
            get;
            private set;
        }

        public List<string> TargetNames
        {
            get;
            private set;
        }

        public List<string> RemoveNames
        {
            get;
            private set;
        }

        public object UserData
        {
            get;
            private set;
        }

        public int RemoveCount
        {
            get
            {
                return RemoveNames.Count;
            }
        }

        public int TargetCount
        {
            get
            {
                return TargetNames.Count;
            }
        }

        public bool RemoveAll
        {
            get
            {
                if (RemoveNames == null)
                    return true;
                return RemoveCount == 0;
            }
        }

        public string DefaultTarget
        {
            get
            {
                if (TargetCount == 0)
                    return string.Empty;
                return TargetNames[0];
            }
        }

        public string DefaultRemove
        {
            get
            {
                if (RemoveNames.Count == 0)
                    return string.Empty;
                return RemoveNames[0];
            }
        }
        /// <summary>
        /// 移除当前所有场景，加载targetNames所有场景
        /// </summary>
        /// <param name="targetNames"></param>
        /// <param name="transitionType"></param>
        /// <param name="userData"></param>
        /// <returns></returns>
        public static DoTransitionCompleteEventArgs Create(List<string> targetNames, SceneTransitionType transitionType, object userData)
        {
            DoTransitionCompleteEventArgs doTransitionCompleteEventArgs = ReferencePool.Acquire<DoTransitionCompleteEventArgs>();
            doTransitionCompleteEventArgs.TransitionType = transitionType;
            doTransitionCompleteEventArgs.TargetNames = targetNames;
            doTransitionCompleteEventArgs.UserData = userData;
            return doTransitionCompleteEventArgs;
        }

        /// <summary>
        /// 移除当前所有场景，加载targetName场景
        /// </summary>
        /// <param name="targetName"></param>
        /// <param name="transitionType"></param>
        /// <param name="userData"></param>
        /// <returns></returns>
        public static DoTransitionCompleteEventArgs Create(string targetName, SceneTransitionType transitionType, object userData)
        {
            DoTransitionCompleteEventArgs doTransitionCompleteEventArgs = ReferencePool.Acquire<DoTransitionCompleteEventArgs>();
            doTransitionCompleteEventArgs.TransitionType = transitionType;
            doTransitionCompleteEventArgs.TargetNames.Add(targetName);
            doTransitionCompleteEventArgs.UserData = userData;
            return doTransitionCompleteEventArgs;
        }

        /// <summary>
        /// 移除removeNames所有场景，加载targetNames所有场景
        /// </summary>
        /// <param name="targetNames"></param>
        /// <param name="removeNames"></param>
        /// <param name="transitionType"></param>
        /// <param name="userData"></param>
        /// <returns></returns>
        public static DoTransitionCompleteEventArgs Create(List<string> targetNames, List<string> removeNames, SceneTransitionType transitionType, object userData)
        {
            DoTransitionCompleteEventArgs doTransitionCompleteEventArgs = ReferencePool.Acquire<DoTransitionCompleteEventArgs>();
            doTransitionCompleteEventArgs.TransitionType = transitionType;
            doTransitionCompleteEventArgs.TargetNames = targetNames;
            doTransitionCompleteEventArgs.RemoveNames = removeNames;
            doTransitionCompleteEventArgs.UserData = userData;
            return doTransitionCompleteEventArgs;
        }

        /// <summary>
        /// 移除removeName场景，加载targetNames所有场景
        /// </summary>
        /// <param name="targetNames"></param>
        /// <param name="removeName"></param>
        /// <param name="transitionType"></param>
        /// <param name="userData"></param>
        /// <returns></returns>
        public static DoTransitionCompleteEventArgs Create(List<string> targetNames, string removeName, SceneTransitionType transitionType, object userData)
        {
            DoTransitionCompleteEventArgs doTransitionCompleteEventArgs = ReferencePool.Acquire<DoTransitionCompleteEventArgs>();
            doTransitionCompleteEventArgs.TransitionType = transitionType;
            doTransitionCompleteEventArgs.TargetNames = targetNames;
            doTransitionCompleteEventArgs.RemoveNames.Add(removeName);
            doTransitionCompleteEventArgs.UserData = userData;
            return doTransitionCompleteEventArgs;
        }

        /// <summary>
        /// 移除removeName场景，加载targetName场景
        /// </summary>
        /// <param name="targetName"></param>
        /// <param name="removeName"></param>
        /// <param name="transitionType"></param>
        /// <param name="userData"></param>
        /// <returns></returns>
        public static DoTransitionCompleteEventArgs Create(string targetName, string removeName, SceneTransitionType transitionType, object userData)
        {
            DoTransitionCompleteEventArgs doTransitionCompleteEventArgs = ReferencePool.Acquire<DoTransitionCompleteEventArgs>();
            doTransitionCompleteEventArgs.TransitionType = transitionType;
            doTransitionCompleteEventArgs.TargetNames.Add(targetName);
            doTransitionCompleteEventArgs.RemoveNames.Add(removeName);
            doTransitionCompleteEventArgs.UserData = userData;
            return doTransitionCompleteEventArgs;
        }

        /// <summary>
        /// 移除removeNames所有场景，加载targetName场景
        /// </summary>
        /// <param name="targetName"></param>
        /// <param name="removeNames"></param>
        /// <param name="transitionType"></param>
        /// <param name="userData"></param>
        /// <returns></returns>
        public static DoTransitionCompleteEventArgs Create(string targetName, List<string> removeNames, SceneTransitionType transitionType, object userData)
        {
            DoTransitionCompleteEventArgs doTransitionCompleteEventArgs = ReferencePool.Acquire<DoTransitionCompleteEventArgs>();
            doTransitionCompleteEventArgs.TransitionType = transitionType;
            doTransitionCompleteEventArgs.TargetNames.Add(targetName);
            doTransitionCompleteEventArgs.RemoveNames = removeNames;
            doTransitionCompleteEventArgs.UserData = userData;
            return doTransitionCompleteEventArgs;
        }

        public override void Clear()
        {
            // Log.Success("DoTransitionCompleteEventArgs Cleared");
            TransitionType = SceneTransitionType.Immediately;
            TargetNames?.Clear();
            RemoveNames?.Clear();
            UserData = null;
        }

        public void ManuallySetRemoveNames(List<string> removeNames)
        {
            RemoveNames = removeNames;
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("\n");
            stringBuilder.Append("<b>DoTransitionCompleteEventArgs</b> ");
            stringBuilder.Append("Targets: ");
            for (int i = 0; i < TargetNames.Count; i++)
            {
                stringBuilder.Append(TargetNames[i]);
                stringBuilder.Append(" ");
            }
            stringBuilder.Append("\n");

            stringBuilder.Append("Removes: ");
            for (int i = 0; i < RemoveNames.Count; i++)
            {
                stringBuilder.Append(RemoveNames[i]);
                stringBuilder.Append(" ");
            }
            stringBuilder.Append("\n");
            stringBuilder.Append("TransitionType: ");
            stringBuilder.Append(System.Enum.GetName(typeof(SceneTransitionType), TransitionType));
            stringBuilder.Append("\n");
            stringBuilder.Append("UserData: ");
            stringBuilder.Append(UserData);
            return stringBuilder.ToString();
        }
    }
}
