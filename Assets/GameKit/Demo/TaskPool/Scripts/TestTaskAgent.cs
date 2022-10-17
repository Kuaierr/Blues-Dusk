using UnityEngine;
using GameKit;
namespace UnityGameKit.Demo
{
    public class TestTaskAgent : ITaskAgent<TestTaskBase>
    {
        private TestTaskBase m_Task;
        private string m_InventoryHelperTypeName = "DefaultTestTaskAgentHelper";
        private TestTaskAgentHelperBase taskAgentHelper;
        private TestTaskAgentHelperBase customHelper;
        public TestTaskBase Task
        {
            get
            {
                return m_Task;
            }
        }

        public void Initialize()
        {

        }

        public void SetHelper(TestTaskAgentHelperBase helper)
        {
            taskAgentHelper = helper;
        }

        public void Shutdown()
        {

        }

        public StartTaskStatus Start(TestTaskBase task)
        {
            m_Task = task;
            m_Task.Done = false;
            taskAgentHelper.CallHelper("Helper Arg");
            task.OnTaskStart(this, "Start Arg");
            return StartTaskStatus.CanResume;
        }

        public void Update(float elapseSeconds, float realElapseSeconds)
        {
            m_Task.OnTaskUpdate(this, "Update Arg");
            m_Task.Done = true;
        }

        public void Reset()
        {
            
        }
    }
}