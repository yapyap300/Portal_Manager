using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flow_EndUI : Flow_Base
{
    [SerializeField] GameObject endUI;
    bool isEnd;
    public override void Enter()
    {
        endUI.SetActive(true);
    }

    public override void Excute(Flow_Control manager)
    {
        if (isEnd)
            manager.SetNextFlow();
    }

    public override void Exit()
    {
    }
    public void Next()
    {
        isEnd = true;
    }
}
