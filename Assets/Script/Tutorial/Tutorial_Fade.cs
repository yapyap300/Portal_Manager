using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_Fade : Tutorial_Base
{
    [SerializeField] private Fade_Image fadeImage;
    [SerializeField] private bool isFadein;
    [SerializeField] private float fadetime;
    [SerializeField] private float alpha;
    private bool isEnd;
    public override void Enter()
    {
        if (isFadein)
        {
            fadeImage.FadeIn(AfterFade,alpha,fadetime);
        }
        else
        {
            fadeImage.FadeOut(AfterFade, fadetime);
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
