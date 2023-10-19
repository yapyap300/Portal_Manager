using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial_Set1 : Tutorial_Base
{
    [SerializeField] GameObject[] setUI;    

    public override void Enter()
    {
        foreach(GameObject gameObject in setUI)
        {
            gameObject.SetActive(true);
        }
    }

    public override void Excute(Tutorial_Control manager)
    {
        manager.SetNextTutorial();
    }

    public override void Exit()
    {
        
    }
}
