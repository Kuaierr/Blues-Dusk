using GameKit.DataNode;
namespace GameKit.Dialog
{
    public interface IDialogTreeParseHelper 
    {
        void Phase(string rawData, IDialogTree dialogTree);
    }
}