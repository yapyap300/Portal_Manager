using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial_Loading : Tutorial_Base
{
    [SerializeField] private Fade_Loading fadeImage;
    [SerializeField] private bool isFadein;
    private bool isEnd;
    public override void Enter()
    {        
        fadeImage.FadeLoadIn(AfterFade);        
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
        fadeImage.StartCoroutine(fadeImage.TextAnimation(1));
    }
}
