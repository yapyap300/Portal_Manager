using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unknown : Flow_Base
{
    [SerializeField] private GameObject EventFlow;
    public override void Enter()
    {
        DOVirtual.DelayedCall(300f, () => EventFlow.SetActive(true), false);
    }

    public override void Excute(Flow_Control manager)
    {
        manager.SetNextFlow();
    }

    public override void Exit()
    {
        
    }
}
