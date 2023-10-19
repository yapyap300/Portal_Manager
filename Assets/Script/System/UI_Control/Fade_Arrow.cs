using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fade_Arrow : MonoBehaviour
{
    private Image fadeImage;    // 페이드 효과에 사용되는 Image UI

    private void Awake()
    {
        fadeImage = GetComponent<Image>();
    }

    private void OnEnable()
    {
        fadeImage.DOFade(0, 2f).SetLoops(-1,LoopType.Yoyo);
    }
    private void OnDisable()
    {
        fadeImage.color = Color.white;
        fadeImage.DOKill();
    }
}
