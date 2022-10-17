using UnityEngine;
using GameKit;
namespace UnityGameKit.Demo
{
    public class DefaultTestTaskAgentHelper : TestTaskAgentHelperBase
    {
        public override void CallHelper(string arg)
        {
            Utility.Debugger.LogSuccess("Call Example Helper with Arg: {0}", arg);
        }
    }
}