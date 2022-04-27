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

        public virtual void OnTick() { }
        public virtual void OnStart() { }
    }
}

