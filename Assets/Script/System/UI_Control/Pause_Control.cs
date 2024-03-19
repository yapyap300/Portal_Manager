using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Pause_Control : MonoBehaviour
{
    [SerializeField] GameObject[] panels;
    [SerializeField] Transform[] areaPanel;//각 스테이지의 구역정보를 갱신하여 보여줌
    [SerializeField] Transform[] vipPanel;//vip의 정보를 보여줌
    [SerializeField] Text time;// 스테이지의 시간을 보여줌
    [SerializeField] Transform[] errorControl;//그냥 연출용
    [SerializeField] Sprite[] vipSprite;
    [SerializeField] string[] vipDescription;
    private Sequence error;
    void Awake()
    {
        error = DOTween.Sequence();
        error.Append(errorControl[0].DOShakePosition(1,3)).Join(errorControl[1].GetComponent<Image>().DOColor(Color.red, 0.15f)
            .SetEase(Ease.Linear).SetLoops(6, LoopType.Yoyo)).SetUpdate(true).SetAutoKill(false);
    }
    void Update()
    {
        if(!GameManager.Instance.isEvent && Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameManager.Instance.isStop)
            {
                SoundManager.Instance.PlaySfx("PopupClose");
                errorControl[0].gameObject.SetActive(false);
                GameManager.Instance.Resume();
            }
            else
            {
                SoundManager.Instance.PlaySfx("PopupOpen");
                int hour = Mathf.FloorToInt(GameManager.Instance.time / (GameManager.Instance.maxTime * 5)) + 8;
                int min = Mathf.FloorToInt(GameManager.Instance.time * 12 / GameManager.Instance.maxTime) % 60;
                time.text = $"{hour} : {min:D2}";
                Open();
                errorControl[0].gameObject.SetActive(true);
                GameManager.Instance.Stop();
            }
        }
    }
    public void Init()//이 부분은 각 스테이지 시작에 한번 갱신해놓으면 되는거라서 public후 초기화과정에서 사용 중간 이벤트에서도 활용
    {
        if (GameManager.Instance.timeView)
            time.gameObject.SetActive(true);
        for(int index = 0; index < 4; index++)
        {
            if (index < GameManager.Instance.maxDestination)
            {
                areaPanel[index].GetChild(0).GetComponent<Image>().sprite = GameManager.Instance.areaSprites[GameManager.Instance.portalArea[index]];                
            }
            if (index < GameManager.Instance.vipIndex)
            {
                vipPanel[index].GetChild(1).GetComponent<Image>().sprite = vipSprite[index];
                vipPanel[index].GetChild(2).GetChild(0).GetComponent<Text>().text = LocalizationSettings.StringDatabase.GetLocalizedString("Main", vipDescription[index], GameManager.Instance.currentLocale);
            }
        }
    }
    public void Area(GameObject areaPanel)//구역 정보는 업그레이드 후 확인가능
    {
        if (GameManager.Instance.areaView)
        {
            IconSound();
            areaPanel.SetActive(true);
        }
        else
            Error();
    }
    public void VipInfo(GameObject VipInfoPanel)//Vip등장전에는 눌러도 안됨
    {
        if (GameManager.Instance.vipIndex < 1)
            Error();
        else
        {
            IconSound();
            VipInfoPanel.SetActive(true);
        }
    }
    public void QuitGame()
    {
        SoundManager.Instance.StopBGM();
        SceneManager.LoadScene(0);
    }
    private void Open()
    {
        for(int index = 0; index < panels.Length; index++)        
            panels[index].SetActive(false);        
    }
    public void IconSound()
    {
        SoundManager.Instance.PlaySfx("Icon");
    }
    private void Error()//허용되지 않은 버튼을 누르면 실행
    {
        SoundManager.Instance.PlaySfx("Bip");
        error.Restart();
    }
}
