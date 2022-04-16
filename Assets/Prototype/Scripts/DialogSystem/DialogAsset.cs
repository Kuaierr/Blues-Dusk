using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogAsset", menuName = "GameMain/DialogAsset", order = 0)]
public class DialogAsset : ScriptableObject
{
    [TextArea]public string contents;
}
