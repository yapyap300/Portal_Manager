using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flow_Fade : Flow_Base
{
    [SerializeField] private Fade_Image fadeImage;
    [SerializeField] private bool isFadein;
    private bool isEnd;
    public override void Enter()
    {
        if (isFadein)
        {
            fadeImage.FadeIn(AfterFade, 1f);
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
    public override void Excute(Flow_Control manager)
    {
        if (isEnd)
        {
            manager.SetNextFlow();
        }
    }

    public override void Exit()
    {
    }
}
