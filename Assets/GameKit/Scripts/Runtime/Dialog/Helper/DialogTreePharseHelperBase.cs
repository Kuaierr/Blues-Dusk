using GameKit.DataNode;
using GameKit.Dialog;
using UnityEngine;
namespace UnityGameKit.Runtime
{
    public abstract class DialogTreePharseHelperBase: MonoBehaviour, IDialogTreeParseHelper
    {
        public abstract void Phase(string rawData, IDialogTree dialogTree);
    }
}