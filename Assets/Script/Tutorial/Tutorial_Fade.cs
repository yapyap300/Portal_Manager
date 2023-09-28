using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_Fade : Tutorial_Base
{
    [SerializeField] private Fade_Image fadeImage;
    [SerializeField] private bool isFadein;
    private bool isEnd;
    public override void Enter()
    {
        if (isFadein)
        {
            fadeImage.FadeIn(AfterFade,0.8f);
        }
        else
        {
            fadeImage.FadeOut(AfterFade);
        }
    }
    private void AfterFade()
    {
        isEnd = true;
    }
    public override void Excute(Tutorial_Control manager)
    {
        if (isEnd)
        {
            manager.SetNextTutorial();
        }
    }

    public override void Exit()
    {        
    }
}
