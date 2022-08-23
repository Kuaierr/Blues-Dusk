using UnityEngine;
using GameKit;
using UnityGameKit.Runtime;
namespace UnityGameKit.Demo
{
    public class TestTaskEntry : MonoBehaviour
    {
        [SerializeField] private string m_TestTaskAgentHelper = "DefaultTestTaskAgentHelper";
        private TaskPool<TestTaskBase> m_TaskPool;
        private TestTaskAgent testTaskAgent;
        private TestTaskAgentHelperBase taskAgentHelper;
        private void Start()
        {
            taskAgentHelper = Helper.CreateHelper<TestTaskAgentHelperBase>(m_TestTaskAgentHelper, null);
            taskAgentHelper.name = "Test Task Agent Helper";
            taskAgentHelper.transform.SetParent(this.transform);

            testTaskAgent = new TestTaskAgent();
            testTaskAgent.SetHelper(taskAgentHelper);

            m_TaskPool = new TaskPool<TestTaskBase>();
            m_TaskPool.AddAgent(testTaskAgent);
        }


        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                TestTaskBase mainTask = TestTaskBase.Create(Time.fixedUnscaledTime);
                m_TaskPool.AddTask(mainTask);
            }

            m_TaskPool.Update(0, 0);
            // m_TaskPool.AddAgent
        }
    }
}