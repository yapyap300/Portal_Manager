using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Fade_Loading : MonoBehaviour
{
    [SerializeField] private Image fadeImage;
    [SerializeField] private Text text;
    Color color = new(0, 0, 0, 0);

    private void Awake()
    {
        fadeImage = GetComponent<Image>();
    }
    public void FadeLoadIn(UnityAction action)
    {
        fadeImage.DOFade(1, 1).OnComplete(() => action());
    }
    public void FadeLoadOut()
    {
        fadeImage.color = color;        
    }
    public IEnumerator TextAnimation(int i)
    {
        text.DOText($"{i}일차 출근.....",2);
        yield return new WaitForSeconds(0.5f);
        FadeLoadOut();
    }
}
