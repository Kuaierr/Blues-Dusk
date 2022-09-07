
using System.Collections.Generic;
namespace GameKit.Dialog
{
    public interface IDialogOption
    {
        int Id { get; }
        string Text { get; }
        bool HasCondition { get; }
        Dictionary<string, int> DiceConditions { get; }
    }
}