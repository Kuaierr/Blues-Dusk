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
        void SetDialogHelper(IDialogTreeParseHelper helper);
        IDialogOptionSet CreateOptionSet(IDataNode node);
        void GetOrCreatetDialogTree(string treeName);
    }
}