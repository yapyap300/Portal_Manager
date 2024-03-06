using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flow_Fade : Flow_Base
{
    [SerializeField] private Fade_Image fadeImage;
    [SerializeField] private bool isFadein;
    [SerializeField] private float fadeTime;
    [SerializeField] private float alpha;
    private bool isEnd;
    public override void Enter()
    {
        if (isFadein)
        {
            fadeImage.FadeIn(AfterFade, alpha, fadeTime);
        }
        else
        {
            fadeImage.FadeOut(AfterFade, fadeTime);
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
        isEnd = false;
    }
}
