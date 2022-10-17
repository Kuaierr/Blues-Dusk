using UnityEngine;
using GameKit;
using UnityGameKit.Runtime;
namespace UnityGameKit.Demo
{
    public class TestTaskBase : TaskBase
    {
        private int m_StartTime;
        public virtual void OnTaskStart(TestTaskAgent agent, string arg0)
        {
            Utility.Debugger.LogSuccess("Call Start by Agent {0} at {1} with {2}.", agent, m_StartTime, arg0);
        }

        public virtual void OnTaskUpdate(TestTaskAgent agent, string arg0)
        {
            Utility.Debugger.LogSuccess("Call Update by Agent {0} at {1} with {2}.", agent, m_StartTime, arg0);
        }

        public TestTaskBase()
        {
            m_StartTime = 0;
        }
        public static TestTaskBase Create(float startTime)
        {
            TestTaskBase task = ReferencePool.Acquire<TestTaskBase>();
            task.m_StartTime = (int)startTime;
            return task;
        }
    }
}