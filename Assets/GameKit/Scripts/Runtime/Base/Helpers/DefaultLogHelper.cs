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
                    Debug.Log(Utility.Text.Format("<b><color=yellow>[Warning] </color></b>{0}", message));
                    break;

                case GameKitLogType.Error:
                    Debug.LogError(Utility.Text.Format("<b><color=red>[Error] </color></b>{0}", message));
                    break;
                
                case GameKitLogType.Success:
                    Debug.Log(Utility.Text.Format("<b><color=green>[Success] </color></b>{0}", message));
                    break;
                
                case GameKitLogType.Fail:
                    Debug.Log(Utility.Text.Format("<b><color=#B22222>[Fail] </color></b>{0}", message));
                    break;

                default:
                    throw new GameKitException(Utility.Text.Format("<b><color=#800000>[Fatal] </color></b>{0}", message));
            }
        }
    }
}
