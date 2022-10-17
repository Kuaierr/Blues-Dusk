using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityGameKit.Runtime;

namespace GameKit.QuickCode
{
    public class UIData : UIBehaviour
    {
        [HideInInspector] public RectTransform rectTransform;
        protected UIPanel group;
        public UIPanel Group
        {
            get
            {
                return group;
            }
            set
            {
                group = value;
            }
        }
        /// <summary>
        /// Awake is called when the script instance is being loaded.
        /// </summary>
        protected override void Awake()
        {
            rectTransform = GetComponent<RectTransform>();
        }
        protected override void Start()
        {

            OnStart();
        }
        public virtual void OnTick() { }
        public virtual void OnStart() { }
        public virtual void OnUpdate() { }
    }
}

