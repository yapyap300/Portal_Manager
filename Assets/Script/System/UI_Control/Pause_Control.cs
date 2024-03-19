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
    [SerializeField] Transform[] areaPanel;//�� ���������� ���������� �����Ͽ� ������
    [SerializeField] Transform[] vipPanel;//vip�� ������ ������
    [SerializeField] Text time;// ���������� �ð��� ������
    [SerializeField] Transform[] errorControl;//�׳� �����
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
    public void Init()//�� �κ��� �� �������� ���ۿ� �ѹ� �����س����� �Ǵ°Ŷ� public�� �ʱ�ȭ�������� ��� �߰� �̺�Ʈ������ Ȱ��
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
    public void Area(GameObject areaPanel)//���� ������ ���׷��̵� �� Ȯ�ΰ���
    {
        if (GameManager.Instance.areaView)
        {
            IconSound();
            areaPanel.SetActive(true);
        }
        else
            Error();
    }
    public void VipInfo(GameObject VipInfoPanel)//Vip���������� ������ �ȵ�
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
    private void Error()//������ ���� ��ư�� ������ ����
    {
        SoundManager.Instance.PlaySfx("Bip");
        error.Restart();
    }
}
