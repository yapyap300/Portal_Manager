using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial_Set2 : Tutorial_Base
{
    [SerializeField] Image count;
    public override void Enter()
    {
        count.fillAmount = 1;
    }

    public override void Excute(Tutorial_Control manager)
    {
        manager.SetNextTutorial();
    }

    public override void Exit()
    {

    }
}
