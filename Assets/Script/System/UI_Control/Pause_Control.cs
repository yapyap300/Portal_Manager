using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pause_Control : MonoBehaviour
{
    [SerializeField] Transform[] areaPanel;//�� ���������� ���������� �����Ͽ� ������
    [SerializeField] Transform[] vipPanel;//vip�� ������ ������
    [SerializeField] Text time;// ���������� �ð��� ������
    [SerializeField] Transform[] errorControl;//�׳� �����
    private Sequence error;

    void Awake()
    {
        error = DOTween.Sequence();
        error.Append(errorControl[0].DOShakePosition(1,3)).Join(errorControl[1].GetComponent<Image>().DOColor(Color.red, 0.15f)
            .SetEase(Ease.Linear).SetLoops(6, LoopType.Yoyo)).SetUpdate(true).SetAutoKill(false);
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
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
            areaPanel[index].GetChild(0).GetComponent<Image>().sprite = GameManager.Instance.areaSprites[GameManager.Instance.portalArea[index]];
            if(index < GameManager.Instance.maxDestination)
                areaPanel[index].gameObject.SetActive(true);
            if (index < GameManager.Instance.vipIndex)
                vipPanel[index].gameObject.SetActive(true);
        }
    }
    public void Area(GameObject areaPanel)//���� ������ ���׷��̵� �� Ȯ�ΰ���
    {
        if (GameManager.Instance.areaView)
        {
            SoundManager.Instance.PlaySfx("Icon");
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
            SoundManager.Instance.PlaySfx("Icon");
            VipInfoPanel.SetActive(true);
        }
    }
    public void QuitGame()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }
    private void Error()//������ ���� ��ư�� ������ ����
    {
        SoundManager.Instance.PlaySfx("Bip");
        error.Restart();
    }
}
