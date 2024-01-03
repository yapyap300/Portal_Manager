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
    public void End()//vip�� �̺�Ʈ �ִϸ��̼ǿ� �Լ�ȣ��� ���
    {
        isEnd= true;
    }
}
