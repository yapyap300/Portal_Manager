using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    [Header("# Game Control")]
    [SerializeField] private Material mixBackground;
    [SerializeField] private GameObject endFlow;//�������� ������ Ȱ��ȭ��ų �帧
    [SerializeField] private Stage_Data[] stages;
    [SerializeField] private GameObject vipControl;
    [SerializeField] private GameObject backgroundControl;
    public Locale currentLocale;
    public People_Control movePeople;
    public Portal[] portals;
    public Kickout kickout;
    public Sprite[] areaSprites;//�� ������ ������ ������� ������ �־ �ٸ� ������ �����ϰ� �Ѵ�.
    public int[] portalToArea;
    public int stageIndex;
    public float time; 
    public bool isStop;
    public bool isEvent;
    public int maxTime;
    [Header("# Game Total Info")]//������ Ŭ���� ������ �� ���� ��� ���ӿ����� ���������� ��ġ�� ���� ���
    public int wrongCount;
    public int wrongArea;
    public int totalPay;
    public int vipScore;
    public int gameoverCount;
    [Header("# Stage Event Info")]
    public bool isMix;
    public bool isInactive;
    public int InactiveIndex;
    [Header("# Stage Control")]
    public int[] portalArea;
    public int maxDestination;//�������� ���� ������������ �ٸ� ������ ������
    public int vipIndex;//vip�� ���°���� �����ߴ��� �����Ҷ� ���� ����
    public int maxCount;//�������� �ִ� ����
    [Header("# Money Control")]//�� ��꿡 ���� ������
    public int money;
    public int endMoney;
    public int defultPay;
    public int count;
    public int countPenalty;
    public int areaPenalty;
    public int bonus;
    [Header("# Upgrade Info")]
    public bool timeView;//���� ���������� �ð��� �� �� �ִ� ���׷��̵�
    public int countless;//������ ��������
    public bool isCountNumber;//������ ���ڷ� �����ֱ�
    public int waitTime;//������ �簡�� ��Ÿ��
    public bool autoBan;//�ڵ� ��
    public bool areaView;//���� ǥ�� �� Ű�� �� �������� ���� ������ ����ϴ��� Ȯ�κҰ� ��Ʋ���� ������
    public int vipWaitTime;//VIP�� ���ð� ����
    public static GameManager Instance
    {
        get
        {
            return instance;
        }
    }
    void Awake()
    {
        if (instance == null)        
            instance = this;       
    }
    void Start()
    {
        currentLocale = LocalizationSettings.SelectedLocale;
        Stop();
        StageInit();
        Resume();
    }
    void Update()
    {
        if (isStop) return;
        time += Time.deltaTime;
        if(time >= maxTime * 60f)
        {
            endFlow.SetActive(true);
        }
    }
    public void Stop()
    {
        Time.timeScale = 0;
        isStop = true;
        SoundManager.Instance.StopBGM();
    }
    public void Resume()
    {
        Time.timeScale = 1;
        isStop = false;
        SoundManager.Instance.PlayBGM();
    }
    public void StageInit()//�Լ��� �����ΰ� endflow���� ���׷��̵� ������ �����ų ����
    {
        SoundManager.Instance.SetBGM($"BGM0{(stageIndex % 3) + 1}");
        stageIndex++;
        defultPay = 0;
        count = 0;
        countPenalty = 0;
        areaPenalty = 0;
        bonus = 0;
        time = 0;
        isMix = stages[stageIndex].isMix;        
        isInactive = false;
        maxDestination = stages[stageIndex].portalCount;
        maxTime = stages[stageIndex].stageTime;
        maxCount = 25 - (countless * 3);
        vipIndex = stages[stageIndex].vipIndex;
        kickout.auto = autoBan;
        if (isMix)
            RandomArea();
        for (int index = 0; index < maxDestination; index++)
        {
            portals[index].Init(portalArea[index]);
            if (!stages[stageIndex].portalActive[index])
            {
                isInactive = true;
                InactiveIndex = index;
            }
            else            
                portals[index].gameObject.SetActive(true);
        }
        kickout.gameObject.SetActive(true);
        if(vipIndex > 0)
            vipControl.SetActive(true);        
        movePeople.Init();
        if (isMix)
        {
            backgroundControl.GetComponent<SpriteRenderer>().material = mixBackground;
            SoundManager.Instance.PlaySfx("Mix");
        }
        backgroundControl.SetActive(true);
    }
    public void StageEvent()//����鼭 ���� ��ȭ�� �־����� �ᱹ �������� �ʹݿ��� ��ȭ������ �̺�Ʈ�� ����Ǵ� �� ������ �Ѱ�� �̷��Ը� �ص� �ɵ��ϴ� �������� �߰��� �̺�Ʈ�� ������ �ʿ��ϴٸ� �׳� ������Ʈ �ϳ��� ���� ������ �����Ҷ����� ����ϴ� �����ϸ� �̺�Ʈ �÷ο츦 Ȱ��ȭ �ϴ� ������ �ص� �ɵ��ϴ�.
    {
        if (stages[stageIndex].stageEvent != null)
            stages[stageIndex].stageEvent.SetActive(true);
    }
    public void StageEnd()//�ð��� �ٵǼ� endflow�� Ű�� �߸� �۵��� ������ �ִ� ������Ʈ�� �ϴ� �� ��Ȱ��ȭ�� ���� �׸��� �������� ��� ���� �� ���
    {
        movePeople.StageClear();
        for (int index = 0; index < 4; index++)
        {
            if (portals[index].gameObject.activeSelf)
                portals[index].EndStage();
            portals[index].gameObject.SetActive(false);
            portalArea[index] = index;
            portalToArea[index] = index;
        }
        countPenalty -= kickout.banPenalty;
        kickout.gameObject.SetActive(false);
        if(vipControl.activeSelf)
            vipControl.SetActive(false);
        backgroundControl.SetActive(false);
        wrongCount += -countPenalty; //Ʋ�� �ο��� ���� ������꺯���� ����
        wrongArea += -areaPenalty;// ���� �߸� ���� ��
        vipScore += bonus;// vip����
    }
    public void RandomArea()//�������� �ε��� ����
    {
        List<int> list = new();
        for(int index = 0; index < maxDestination; index++)
            list.Add(index);
        for (int index = 0; index < maxDestination;)
        {
            int area = list[Random.Range(0, list.Count)];
            if (list.Count > 1 && area == index) continue;//������ ������ ���� ������ ������ �ٽ� �̱� ��� ������ �������� �������� ���Ҷ��� ���� ������ȣ�� ���ι�ȣ�ϼ� �־ �׳� �ٷ� �ִ´�.            
            list.Remove(area);
            portalArea[index] = area;
            portalToArea[area] = index++;//�� �������� ��� ������ ���آZ�ٸ� �� ������ ����� �������� �ε����� ���� ���� ������ �ڼ��Ӱ� ��Ȱ��ȭ�� ���� ������ �̿��Ϸ��� �Ѵ�.            
        }
    }
}
