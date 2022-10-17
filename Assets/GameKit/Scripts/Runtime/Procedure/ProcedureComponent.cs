using GameKit;
using GameKit.Fsm;
using System;
using System.Collections;
using UnityEngine;
using GameKit.Procedure;

namespace UnityGameKit.Runtime
{
    [DisallowMultipleComponent]
    [AddComponentMenu("GameKit/GameKit Procedure Component")]
    public sealed class ProcedureComponent : GameKitComponent
    {
        private IProcedureManager m_ProcedureManager = null;
        private ProcedureBase m_EntranceProcedure = null;
        private GameObject m_DynamicParent = null;

        [SerializeField]
        private string[] m_AvailableProcedureTypeNames = null;

        [SerializeField]
        private string m_EntranceProcedureTypeName = null;
        public string CachedDoorName;

        public ProcedureBase CurrentProcedure
        {
            get
            {
                return m_ProcedureManager.CurrentProcedure;
            }
        }

        public float CurrentProcedureTime
        {
            get
            {
                return m_ProcedureManager.CurrentProcedureTime;
            }
        }

        public Transform DynamicParent
        {
            get
            {
                if (m_DynamicParent == null)
                {
                    m_DynamicParent = GameObject.Find("Dynamic");
                    if (m_DynamicParent == null)
                        return null;
                }
                return m_DynamicParent.transform;
            }
        }


        protected override void Awake()
        {
            base.Awake();

            m_ProcedureManager = GameKitModuleCenter.GetModule<IProcedureManager>();
            if (m_ProcedureManager == null)
            {
                Log.Fatal("Procedure manager is invalid.");
                return;
            }
        }

        private IEnumerator Start()
        {
            ProcedureBase[] procedures = new ProcedureBase[m_AvailableProcedureTypeNames.Length];
            for (int i = 0; i < m_AvailableProcedureTypeNames.Length; i++)
            {
                Type procedureType = Utility.Assembly.GetType(m_AvailableProcedureTypeNames[i]);
                if (procedureType == null)
                {
                    Log.Error("Can not find procedure type '{0}'.", m_AvailableProcedureTypeNames[i]);
                    yield break;
                }

                procedures[i] = (ProcedureBase)Activator.CreateInstance(procedureType);
                if (procedures[i] == null)
                {
                    Log.Error("Can not create procedure instance '{0}'.", m_AvailableProcedureTypeNames[i]);
                    yield break;
                }

                if (m_EntranceProcedureTypeName == m_AvailableProcedureTypeNames[i])
                {
                    m_EntranceProcedure = procedures[i];
                }
            }

            if (m_EntranceProcedure == null)
            {
                Log.Error("Entrance procedure is invalid.");
                yield break;
            }

            m_ProcedureManager.Initialize(GameKitModuleCenter.GetModule<IFsmManager>(), procedures);

            yield return new WaitForEndOfFrame();

            m_ProcedureManager.StartProcedure(m_EntranceProcedure.GetType());
        }


        public bool HasProcedure<T>() where T : ProcedureBase
        {
            return m_ProcedureManager.HasProcedure<T>();
        }


        public bool HasProcedure(Type procedureType)
        {
            return m_ProcedureManager.HasProcedure(procedureType);
        }


        public ProcedureBase GetProcedure<T>() where T : ProcedureBase
        {
            return m_ProcedureManager.GetProcedure<T>();
        }


        public ProcedureBase GetProcedure(Type procedureType)
        {
            return m_ProcedureManager.GetProcedure(procedureType);
        }

        public void StartProcedure<T>() where T : ProcedureBase
        {
            m_ProcedureManager.StartProcedure<T>();
        }

        public void StartProcedure(Type procedureType)
        {
            m_ProcedureManager.StartProcedure(procedureType);
        }

        public void SetData<T>(string dataName, T data) where T : Variable
        {
            m_ProcedureManager.SetData<T>(dataName, data);
        }
        
        public void SetData(string dataName, Variable data)
        {
            m_ProcedureManager.SetData(dataName, data);
        }
    }
}
