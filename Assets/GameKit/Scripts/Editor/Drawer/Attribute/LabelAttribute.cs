using UnityEditor;
using UnityEngine;
using UnityGameKit.Runtime;

namespace UnityGameKit.Editor
{
    public class LabelAttribute : PropertyAttribute
    {
        public string label;
        public LabelAttribute(string label)
        {
            this.label = label;
        }
    }
}