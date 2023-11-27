using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flow_UpgradeUI : Flow_Base
{
    [SerializeField] private GameObject upgradeUI;
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
        throw new System.NotImplementedException();
    }
    public void Next()
    {
        isEnd = true;
    }
}
