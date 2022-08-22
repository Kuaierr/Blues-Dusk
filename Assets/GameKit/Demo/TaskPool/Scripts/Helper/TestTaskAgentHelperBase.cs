using UnityEngine;
public abstract class TestTaskAgentHelperBase : MonoBehaviour, ITestTaskAgentHelper
{
    public abstract void CallHelper(string arg);
}