using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityGameKit.Demo
{
    public class EntityTest : EntityBase
    {
        private EntityTestData m_testdata = null;
        protected override void OnShow(object userData)
        {
            base.OnShow(userData);
            m_testdata = userData as EntityTestData;
            Debug.Log(m_testdata.TestData);
        }
    }
}
