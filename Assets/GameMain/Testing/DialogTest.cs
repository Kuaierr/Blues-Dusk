using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityGameKit.Runtime;


public class DialogTest : MonoBehaviour
{
    public int count = 0;
    [Dialog] public string Dialog;



    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            DialogAction();
        }
    }

    private void DialogAction()
    {
        DialogSystem.current.StartDialog(Dialog);
    }
}
