using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Stage_Upgrade : MonoBehaviour
{
    [SerializeField] private Icon_Data[] iconDatas;
    [SerializeField] private Text[] upgradeValues;
    [SerializeField] private Text currentMoney;
    [SerializeField] private Transform errorControl;
    private Sequence error;
    private int money;
    // Start is called before the first frame update
    void Start()
    {
        error = DOTween.Sequence();
        error.Append(errorControl.GetComponent<Image>().DOColor(Color.red, 0.5f).SetEase(Ease.Linear).SetLoops(6, LoopType.Yoyo)).Join(Camera.main.DOShakeRotation(1f, 1f))
            .SetUpdate(true).SetAutoKill(false);
    }
    void OnEnable()
    {
        Init();
    }

    private void Init()
    {
        money = GameManager.Instance.money;
        currentMoney.text = $"{money:N}원";
        for(int index = 0; index < iconDatas.Length; index++)
        {            
            iconDatas[index].GetComponent<Button>().interactable = iconDatas[index].grade < iconDatas[index].maxGrade;
            if (iconDatas[index].grade == iconDatas[index].maxGrade)
                upgradeValues[index].text = "";
            else
                upgradeValues[index].text = $"{iconDatas[index].value[iconDatas[index].grade]:N}원";
        }
    }
    public void Upgrade(Icon_Data clickObject)//각 업그레이드 버튼들에 할당할 클릭이벤트 함수
    {
        if(money - clickObject.value[clickObject.grade] < 0)
            error.Restart();
        else
        {
            money -= clickObject.value[clickObject.grade];
            clickObject.GetComponent<Button>().interactable = false;
            upgradeValues[clickObject.IconIndex].text = "";
            currentMoney.text = $"{money:N}원";
            clickObject.grade++;
        }
    }
    public void Next()//각 업그레이드 상황 갱신
    {
        GameManager.Instance.money = money;
        GameManager.Instance.countless = iconDatas[0].grade;
        GameManager.Instance.waitTime = iconDatas[1].grade;
        GameManager.Instance.vipColltime = iconDatas[2].grade;
        GameManager.Instance.isCountNumber = iconDatas[3].grade == 1;
        GameManager.Instance.autoBan = iconDatas[4].grade == 1;
        GameManager.Instance.timeView = iconDatas[5].grade == 1;
        GameManager.Instance.vipNumber = iconDatas[6].grade;
        GameManager.Instance.areaView = iconDatas[7].grade == 1;
    }
}
