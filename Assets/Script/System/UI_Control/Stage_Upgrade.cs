using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stage_Upgrade : MonoBehaviour
{
    [SerializeField] private Transform panel;
    [SerializeField] private Icon_Data[] iconDatas;
    [SerializeField] private Text[] upgradeValues;
    [SerializeField] private Text currentMoney;
    [SerializeField] private Transform errorControl;
    private Sequence error;
    private int money;
    // Start is called before the first frame update
    void Awake()
    {
        error = DOTween.Sequence().Append(errorControl.GetComponent<Image>().DOColor(Color.red, 0.5f).SetEase(Ease.Linear).SetLoops(6, LoopType.Yoyo)).Join(panel.DOShakePosition(0.5f, 5f))
            .SetUpdate(true).SetAutoKill(false).Pause();
    }
    void OnEnable()
    {
        Init();
    }

    private void Init()
    {
        money = GameManager.Instance.endMoney;
        currentMoney.text = $"{money:N0}원";
        for(int index = 0; index < iconDatas.Length; index++)
        {            
            iconDatas[index].GetComponent<Button>().interactable = iconDatas[index].grade < iconDatas[index].maxGrade;
            if (iconDatas[index].grade == iconDatas[index].maxGrade)
                upgradeValues[index].text = "";
            else
                upgradeValues[index].text = $"{iconDatas[index].value[iconDatas[index].grade]:N0}원";
        }
        if(GameManager.Instance.vipIndex > 0)
            iconDatas[6].gameObject.SetActive(true);
    }
    public void Upgrade(Icon_Data clickObject)//각 업그레이드 버튼들에 할당할 클릭이벤트 함수
    {
        if (money - clickObject.value[clickObject.grade] < 0)
        {
            SoundManager.Instance.PlaySfx("Bip");
            error.Restart();
        }
        else
        {
            SoundManager.Instance.PlaySfx("Upgrade");
            money -= clickObject.value[clickObject.grade];
            clickObject.GetComponent<Button>().interactable = false;
            upgradeValues[clickObject.IconIndex].text = "";
            currentMoney.text = $"{money:N0}원";
            clickObject.grade++;
        }
    }
    public void Next()//각 업그레이드 상황 갱신
    {
        GameManager.Instance.money = money;
        GameManager.Instance.countless = iconDatas[0].grade;
        GameManager.Instance.waitTime = iconDatas[1].grade;
        GameManager.Instance.areaView = iconDatas[2].grade == 1;
        GameManager.Instance.isCountNumber = iconDatas[3].grade == 1;
        GameManager.Instance.autoBan = iconDatas[4].grade == 1;
        GameManager.Instance.timeView = iconDatas[5].grade == 1;
        GameManager.Instance.vipWaitTime = iconDatas[6].grade;
    }
}
