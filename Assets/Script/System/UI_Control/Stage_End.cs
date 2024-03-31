using DG.Tweening;
using UnityEngine;
using UnityEngine.Localization.Settings;
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
    void Awake()
    {
        textActive = DOTween.Sequence().Append(DOVirtual.DelayedCall(1f, () => TextActive(0))).Append(DOVirtual.DelayedCall(1f, () => TextActive(1)))
            .Append(DOVirtual.DelayedCall(1f, () => TextActive(2))).Append(DOVirtual.DelayedCall(1f, () => TextActive(3)))
            .Append(DOVirtual.DelayedCall(1f, () => TextActive(4))).Append(DOVirtual.DelayedCall(1f, () => TextActive(5)))
            .Append(DOVirtual.DelayedCall(1f, () => { TextActive(6); next.interactable = true;})).SetUpdate(true).SetAutoKill(false).Pause();
    }
    void OnEnable()
    {   //스테이지를 1일부터 10일로하면 흐름상 이상해서 1부터 10을 1부터 30에 맞추는 계산식을 사용함
        day.text = $"{(int)((GameManager.Instance.stageIndex - 1) * (29f / 9f) + 1)}" + LocalizationSettings.StringDatabase.GetLocalizedString("Main", "endTitle", GameManager.Instance.currentLocale);
        ValueUpdate();
        EndPannelIn();
    }
    private void ValueUpdate()
    {
        money = GameManager.Instance.money;
        pay = GameManager.Instance.defultPay + GameManager.Instance.count * (10 + (GameManager.Instance.countless * 5));// 작동 정원을 줄이는 업그레이드를 했을때 더 이득을 볼수있게끔 받는 돈을 늘린다.
        areaPenaltyMoney = GameManager.Instance.areaPenalty * (30 + (GameManager.Instance.countless * 5));// 대신 받는 돈을 늘린만큼 틀렸을때의 패널티도 늘린다.
        countPenaltyMoney = GameManager.Instance.countPenalty * (30 + (GameManager.Instance.countless * 5));
        bonusMoney = GameManager.Instance.bonus * 1000;
        result = money + pay + areaPenaltyMoney + countPenaltyMoney + bonusMoney - tex[GameManager.Instance.stageIndex];
    }
    private void EndPannelIn()
    {
        moneyTexts[0].text = $"{money:N0}";
        moneyTexts[1].text = $"{pay:N0}";
        moneyTexts[2].text = $"{areaPenaltyMoney:N0}";
        moneyTexts[3].text = $"{countPenaltyMoney:N0}";
        moneyTexts[4].text = $"{bonusMoney:N0}";
        moneyTexts[5].text = $"-{tex[GameManager.Instance.stageIndex]:N0}";
        moneyTexts[6].text = $"{result:N0}";
        textActive.Restart();
    }
    public void InActive()//다음 스테이지로 넘어가는 버튼에 적용할 함수
    {
        GameManager.Instance.totalPay += pay + bonusMoney; 
        GameManager.Instance.endMoney = result;
        foreach (Text t in nameTexts)
            t.gameObject.SetActive(false);
    }
    private void TextActive(int index)
    {
        nameTexts[index].gameObject.SetActive(true);
        SoundManager.Instance.PlaySfx("Money");
    }
}
