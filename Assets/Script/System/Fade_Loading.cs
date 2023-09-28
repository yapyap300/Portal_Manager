using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Fade_Loading : MonoBehaviour
{
    [SerializeField] private Image fadeImage;
    [SerializeField] private Image portal;
    [SerializeField] private Text text;
    private Sequence mySequence;
    private int day = 0;
    Color color = new(0, 0, 0, 0);

    private void Awake()
    {
        fadeImage = GetComponent<Image>();
        mySequence.SetAutoKill(false).Pause().Append(text.DOText($"{day}일차", 2, false, ScrambleMode.Custom, "아!출근하기싫다")).Append(portal.DOFade(Mathf.Pow((float)day, 2) * 0.5f, 1).OnStart(() => { }));            
    }
    public void FadeLoadIn(UnityAction action)
    {
        fadeImage.DOFade(1, 1).OnComplete(() => action());
    }
    public void FadeLoadOut()
    {
        fadeImage.color = color;
        fadeImage.gameObject.SetActive(false);        
    }
    public IEnumerator TextAnimation(int i)
    {
        day = i;
        mySequence.Restart();
        yield return new WaitForSeconds(mySequence.Duration() + 1f);
        FadeLoadOut();
    }
}
