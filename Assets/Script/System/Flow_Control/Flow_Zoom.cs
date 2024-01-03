using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flow_Zoom : Flow_Base
{
    [SerializeField] CinemachineVirtualCamera main;
    [SerializeField] CinemachineVirtualCamera zoom;
    [SerializeField] Animator vip;
    bool isEnd = false;
    public override void Enter()
    {
        zoom.MoveToTopOfPrioritySubqueue();
        vip.SetTrigger("event");
    }

    public override void Excute(Flow_Control manager)
    {
        if (isEnd)
            manager.SetNextFlow();
    }

    public override void Exit()
    {
        main.MoveToTopOfPrioritySubqueue();
    }
    public void End()//vip의 이벤트 애니메이션에 함수호출로 사용
    {
        isEnd= true;
    }
}
