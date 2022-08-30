
using System.Collections.Generic;
namespace GameKit.Dialog
{
    public interface IDialogOptionSet
    {
        List<IDialogOption> Options { get; }
        void Release();
    }
}