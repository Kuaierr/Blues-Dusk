using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace GameKit
{
    public class UIForm : UIBehaviour
    {
        public RectTransform rectTransform;
        protected UIGroup group;
        public UIGroup Group
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
        protected override void Start()
        {
            rectTransform = GetComponent<RectTransform>();
            OnStart();
        }
        public virtual void OnTick() { }
        public virtual void OnStart() { }
    }
}

