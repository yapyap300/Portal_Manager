using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Writer : VIP_Base
{
    public override bool NecessaryCondition(Portal portal)
    {
        if (portal.people.Count != GameManager.Instance.maxCount)
            return false;
        return true;
    }

    public override void Process()
    {
        
    }
}
