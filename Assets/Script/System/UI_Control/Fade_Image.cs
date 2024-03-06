using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Fade_Image : MonoBehaviour
{
    [SerializeField] private Image fadeImage;    

    private void Awake()
    {
        fadeImage = GetComponent<Image>();
    }
    public void FadeIn(UnityAction action,float alpha,float time)
    {
        fadeImage.DOFade(alpha,time).SetUpdate(true).SetEase(Ease.InExpo).OnComplete(() => action());
    }
    public void FadeOut(UnityAction action,float time)
    {
        fadeImage.DOFade(0, time).SetUpdate(true).SetEase(Ease.InExpo).OnComplete(() => action());
    }
}
