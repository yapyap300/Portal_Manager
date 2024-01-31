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
    public void FadeIn(UnityAction action,float alpha)
    {
        fadeImage.DOFade(alpha,1.5f).SetUpdate(true).SetEase(Ease.InExpo).OnComplete(() => action());
    }
    public void FadeOut(UnityAction action)
    {
        fadeImage.DOFade(0, 1f).SetUpdate(true).SetEase(Ease.Linear).OnComplete(() => action());
    }
}
