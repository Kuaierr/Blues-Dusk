using GameKit;
using UnityEngine;

namespace UnityGameKit.Runtime
{
    public class DefaultLogHelper : GameKitLog.ILogHelper
    {
        public void Log(GameKitLogType level, object message)
        {
            switch (level)
            {
                case GameKitLogType.Info:
                    Debug.Log(message.ToString());
                    break;

                case GameKitLogType.Warning:
                    Debug.Log(Utility.Text.Format("<color=yellow>[Warning] {0}</color>", message));
                    break;

                case GameKitLogType.Error:
                    Debug.LogError(Utility.Text.Format("<color=red>[Error] {0}</color>", message));
                    break;
                
                case GameKitLogType.Success:
                    Debug.Log(Utility.Text.Format("<color=green>[Success] {0}</color>", message));
                    break;
                
                case GameKitLogType.Fail:
                    Debug.Log(Utility.Text.Format("<color=#B22222>[Fail] {0}</color>", message));
                    break;

                default:
                    throw new GameKitException(Utility.Text.Format("<color=#800000>[Fatal] {0}</color>", message));
            }
        }
    }
}
