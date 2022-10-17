using System;
using System.Collections.Generic;
using UnityEngine;
using GameKit.Fsm;
using UnityGameKit.Runtime;


namespace UnityGameKit.Demo
{
    public class FsmPlayer : MonoBehaviour
    {
        private static int SERIAL_ID = 0;
        protected IFsm<FsmPlayer> fsm;
        protected List<FsmState<FsmPlayer>> stateList;

        private void Awake()
        {
            stateList = new List<FsmState<FsmPlayer>>();
        }
        private void Start()
        {
            CreateFsm();
        }
        
        private void OnDestroy()
        {
            DestroyFsm();   
        }

        private void CreateFsm()
        {
            stateList.Add(new IdleState());
            stateList.Add(new MoveState());
            fsm = GameKitComponentCenter.GetComponent<FsmComponent>().CreateFsm<FsmPlayer>(gameObject.name, this, stateList);
            fsm.Start<IdleState>();
        }

        private void DestroyFsm()
        {
            GameKitComponentCenter.GetComponent<FsmComponent>().DestroyFsm(fsm);
            stateList.Clear();
            fsm = null;
        }
    }
}
