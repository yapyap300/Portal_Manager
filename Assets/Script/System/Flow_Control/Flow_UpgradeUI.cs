using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Flow_UpgradeUI : Flow_Base
{
    [SerializeField] private GameObject upgradeUI;
    [SerializeField] UnityEvent init;
    bool isEnd = false;
    public override void Enter()
    {
        upgradeUI.SetActive(true);
    }

    public override void Excute(Flow_Control manager)
    {
        if (isEnd)
            manager.SetNextFlow();
    }

    public override void Exit()
    {
        init.Invoke();
    }
    public void Next()
    {
        isEnd = true;
    }
}
