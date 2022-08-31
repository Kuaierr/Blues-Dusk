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
        void GetOrCreatetDialogTree(string treeName);
        void CreateDialogTree(string treeName, string content);
        string[] GetLoadedDialogAssetNames();
        IDialogTree GetDialogTree(string treeName);
    }
}