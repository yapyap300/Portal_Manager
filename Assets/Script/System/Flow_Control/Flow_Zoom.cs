using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flow_Zoom : Flow_Base
{
    [SerializeField] GameObject zoom;
    [SerializeField] Animator vip;
    [SerializeField] bool isIn = false;
    [SerializeField] bool isPlay = false;
    public override void Enter()
    {        
        zoom.SetActive(isIn);        
        vip.SetBool("Event",isPlay);        
    }

    public override void Excute(Flow_Control manager)
    {
        manager.SetNextFlow();
    }

    public override void Exit()
    {

    }
}
