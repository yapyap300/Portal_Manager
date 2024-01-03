using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flow_Mix : Flow_Base
{
    [SerializeField] Material mixBack;
    [SerializeField] SpriteRenderer back;
    public override void Enter()
    {
        SoundManager.Instance.PlaySfx("Mix");
        back.material = mixBack;
    }

    public override void Excute(Flow_Control manager)
    {
        manager.SetNextFlow();
    }

    public override void Exit()
    {

    }
}
