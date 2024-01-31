using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Flow_EndUI : Flow_Base
{
    [SerializeField] GameObject endUI;
    bool isEnd;
    public override void Enter()
    {
        GameManager.Instance.StageEnd();
        SoundManager.Instance.SetBGM(3);
        SoundManager.Instance.PlayBGM();
        endUI.SetActive(true);
    }

    public override void Excute(Flow_Control manager)
    {
        if (isEnd)        
            manager.SetNextFlow();               
    }

    public override void Exit()
    {
        isEnd = false;
    }
    public void Next()
    {
        isEnd = true;
    }
}
