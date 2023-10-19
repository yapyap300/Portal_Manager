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
        fadeImage.DOFade(alpha,3.5f).SetEase(Ease.InExpo).OnComplete(() => action());
    }
    public void FadeOut(UnityAction action)
    {
        fadeImage.DOFade(0, 2f).SetEase(Ease.Linear).OnComplete(() => action());
    }
}
