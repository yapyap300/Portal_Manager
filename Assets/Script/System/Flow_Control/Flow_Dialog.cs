using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flow_Dialog : Flow_Base
{
    private Dialog_System dialog_System;
    public override void Enter()
    {
        dialog_System = GetComponent<Dialog_System>();
        dialog_System.Setup();
    }

    public override void Excute(Flow_Control manager)
    {
        bool isEnd = dialog_System.UpdateDialog();

        if (isEnd)
        {
            manager.SetNextFlow();
        }
    }

    public override void Exit()
    {
    }
}
