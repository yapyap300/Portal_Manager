using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idol : VIP_Base
{
    void Awake()
    {
        MoveSpeed = 2f;
    }
    public override bool NecessaryCondition(Portal portal)
    {
        if (portal.people.Count > 0)
            return false;
        return true;
    }

    public override void Process()
    {
        GameManager.Instance.countPenalty -= GameManager.Instance.maxCount - 1;
    }
}
