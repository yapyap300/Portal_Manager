using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stage_End : MonoBehaviour
{    
    [SerializeField] private Text[] nameTexts;
    [SerializeField] private Text[] moneyTexts;
    [SerializeField] private Button next;
    private Sequence textActive;
    private int money;
    private int pay;
    private int areaPenaltyMoney;
    private int countPenaltyMoney;
    private int bonusMoney;
    private int result;

    void Start()
    {
        textActive = DOTween.Sequence();
        textActive.Append(DOVirtual.DelayedCall(0.25f, () => nameTexts[0].gameObject.SetActive(true))).Append(DOVirtual.DelayedCall(0.25f, () => nameTexts[1].gameObject.SetActive(true)))
            .Append(DOVirtual.DelayedCall(0.25f, () => nameTexts[2].gameObject.SetActive(true))).Append(DOVirtual.DelayedCall(0.25f, () => nameTexts[3].gameObject.SetActive(true)))
            .Append(DOVirtual.DelayedCall(0.25f, () => nameTexts[4].gameObject.SetActive(true)))
            .Append(DOVirtual.DelayedCall(0.25f, () => { nameTexts[5].gameObject.SetActive(true); SoundManager.Instance.PlaySfx("Money"); next.interactable = true;})).SetUpdate(true).SetAutoKill(false);
    }
    void OnEnable()
    {
        ValueUpdate();
        EndPannelIn();
    }
    private void ValueUpdate()
    {
        money = GameManager.Instance.money;
        pay = GameManager.Instance.pay;
        areaPenaltyMoney = GameManager.Instance.areaPenaltyMoney;
        countPenaltyMoney = GameManager.Instance.countPenaltyMoney;
        bonusMoney = GameManager.Instance.bonusMoney;
        result = money + pay - areaPenaltyMoney - countPenaltyMoney + bonusMoney;
    }
    private void EndPannelIn()
    {
        moneyTexts[0].text = $"{money:N}";
        moneyTexts[1].text = $"{pay:N}";
        moneyTexts[2].text = $"-{areaPenaltyMoney:N}";
        moneyTexts[3].text = $"-{countPenaltyMoney:N}";
        moneyTexts[4].text = $"{bonusMoney:N}";
        moneyTexts[5].text = $"{result:N}";
        textActive.Restart();
    }
    public void Inactive()//다음 스테이지로 넘어가는 버튼에 적용할 함수
    {
        GameManager.Instance.money = result;
        foreach (Text t in nameTexts)
            t.gameObject.SetActive(false);
    }
}
