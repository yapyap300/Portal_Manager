using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial_End : Tutorial_Base
{
    [SerializeField] private GameObject loadingPanel;
    [SerializeField] private Text fadeText;
    private bool isEnd = false;
    public override void Enter()
    {
        loadingPanel.SetActive(true);
        fadeText.DOFade(0, 1f).SetEase(Ease.InOutBounce).SetLoops(-1, LoopType.Yoyo).SetUpdate(true);
        SoundManager.Instance.StopBGM();
    }

    public override void Excute(Tutorial_Control manager)
    {
        if (isEnd)
            manager.SetNextTutorial();
    }

    public override void Exit()
    {
        loadingPanel.SetActive(false);
        fadeText.DOKill();
    }
    public void Next()
    {
        isEnd = true;
    }
}
