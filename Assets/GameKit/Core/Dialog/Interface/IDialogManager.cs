using System;
using GameKit.DataNode;
using System.Collections.Generic;

namespace GameKit.Dialog
{
    public interface IDialogManager
    {
        event EventHandler<StartDialogSuccessEventArgs> StartDialogSuccess;
        event EventHandler<StartDialogFailureEventArgs> StartDialogFailure;
        event EventHandler<FinishDialogCompleteEventArgs> FinishDialogComplete;
        IDialogOptionSet CreateOptionSet(IDataNode node);
        void SetDialogHelper(IDialogTreeParseHelper helper);
        void GetOrCreatetDialogTree(string treeName, string content = "", object userData = null);
        void CreateDialogTree(string treeName, string content, object userData = null);
        string[] GetLoadedDialogAssetNames();
        IDialogTree GetDialogTree(string treeName);
    }
}