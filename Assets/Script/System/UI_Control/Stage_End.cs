using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stage_End : MonoBehaviour
{
    [SerializeField] private Text day;
    [SerializeField] private Text[] nameTexts;
    [SerializeField] private Text[] moneyTexts;
    [SerializeField] private Button next;
    [SerializeField] private int[] tex;
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
        textActive.Append(DOVirtual.DelayedCall(1f, () => TextActive(0))).Append(DOVirtual.DelayedCall(1f, () => TextActive(1)))
            .Append(DOVirtual.DelayedCall(1f, () => TextActive(2))).Append(DOVirtual.DelayedCall(1f, () => TextActive(3)))
            .Append(DOVirtual.DelayedCall(1f, () => TextActive(4))).Append(DOVirtual.DelayedCall(1f, () => TextActive(5)))
            .Append(DOVirtual.DelayedCall(1f, () => { TextActive(6); next.interactable = true;})).SetUpdate(true).SetAutoKill(false);
    }
    void OnEnable()
    {
        day.text = $"{GameManager.Instance.stageIndex}����";
        ValueUpdate();
        EndPannelIn();
    }
    private void ValueUpdate()
    {
        money = GameManager.Instance.money;
        pay = GameManager.Instance.defultPay + GameManager.Instance.count * 10;
        areaPenaltyMoney = GameManager.Instance.areaPenalty * 30;
        countPenaltyMoney = GameManager.Instance.countPenalty * 30;
        bonusMoney = GameManager.Instance.bonus * 1000;
        result = money + pay - areaPenaltyMoney - countPenaltyMoney + bonusMoney - tex[GameManager.Instance.stageIndex];
    }
    private void EndPannelIn()
    {
        moneyTexts[0].text = $"{money:N}";
        moneyTexts[1].text = $"{pay:N}";
        moneyTexts[2].text = $"-{areaPenaltyMoney:N}";
        moneyTexts[3].text = $"-{countPenaltyMoney:N}";
        moneyTexts[4].text = $"{bonusMoney:N}";
        moneyTexts[5].text = $"-{tex[GameManager.Instance.stageIndex]:N}";
        moneyTexts[6].text = $"{result:N}";
        textActive.Restart();
    }
    public void InActive()//���� ���������� �Ѿ�� ��ư�� ������ �Լ�
    {
        GameManager.Instance.money = result;
        foreach (Text t in nameTexts)
            t.gameObject.SetActive(false);
    }
    public void TextActive(int index)
    {
        nameTexts[index].gameObject.SetActive(true);
        SoundManager.Instance.PlaySfx("Money");
    }
}
