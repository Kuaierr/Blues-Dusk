using GameKit.DataNode;
using GameKit.Dialog;
namespace UnityGameKit.Runtime
{
    public abstract class DialogTreePharseHelperBase: IDialogTreeParseHelper 
    {
        public abstract void Phase(string rawData, IDialogTree dialogTree);
    }
}