using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flow_VipFast : Flow_Base
{
    [SerializeField] VIP_Control obj;
    public override void Enter()
    {
        obj.HurryUp();
    }

    public override void Excute(Flow_Control manager)
    {
        manager.SetNextFlow();
    }

    public override void Exit()
    {

    }
}
