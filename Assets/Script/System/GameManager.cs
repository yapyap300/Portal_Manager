using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    [Header("# Game Control")]
    [SerializeField] private GameObject endFlow;//�������� ������ Ȱ��ȭ��ų �帧
    [SerializeField] private Stage_Data[] stages;
    [SerializeField] private GameObject vipControl;
    public People_Control movePeople;
    public Portal[] portals;
    public Kickout kickout;
    public Sprite[] areaSprites;//�� ������ ������ ������� ������ �־ �ٸ� ������ �����ϰ� �Ѵ�.
    public int stageIndex;
    public float time;
    public int money;    
    public bool isStop;
    public int maxTime;
    [Header("# Stage Control")]
    public int afterNoon;
    public int night;
    public int maxDestination;//�������� ���� ������������ �ٸ� ������ ������
    public int vipIndex;//vip�� ���°���� �����ߴ��� �����Ҷ� ���� ����
    public int maxCount;//�������� �ִ� ����
    [Header("# Stage End Control")]//�� ��꿡 ���� ������
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
        {
            instance = this;
            StageInit();
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Update()
    {
        if (isStop) return;
        time += Time.deltaTime;
        if(time >= maxTime * 60f)
        {
            StageEnd();
            endFlow.SetActive(true);
        }
    }
    public void Stop()
    {
        Time.timeScale = 0;
        isStop = true;
    }
    public void Resume()
    {
        Time.timeScale = 1;
        isStop = false;
    }
    public void StageInit()//�Լ��� �����ΰ� endflow���� ���׷��̵� ������ �����ų ����
    {
        stageIndex++;
        maxDestination = stages[stageIndex].portalCount;
        maxTime = stages[stageIndex].stageTime;
        maxCount = 25 - (countless * 3);
        vipIndex = stages[stageIndex].vipIndex;
        kickout.auto = autoBan;
        for (int index = 0; index < maxDestination; index++)
        {
            if (stages[stageIndex].portalActive[index])
            {
                portals[index].gameObject.SetActive(true);
                portals[index].Init(stages[stageIndex].portalArea[index]);
            }
        }
        kickout.gameObject.SetActive(true);
        if(vipIndex > 0)
            vipControl.SetActive(true);
        defultPay = 0;
        count = 0;
        countPenalty = 0;
        areaPenalty = 0;
        bonus = 0;        
        time = 0;
        afterNoon = (maxTime / 3) * 60;
        night = ((maxTime / 3) * 2) * 60;
        movePeople.Init();        
    }
    public void StageEvent()//����鼭 ���� ��ȭ�� �־����� �ᱹ �������� �ʹݿ��� ��ȭ������ �̺�Ʈ�� ����Ǵ� �� ������ �Ѱ�� �̷��Ը� �ص� �ɵ��ϴ� �������� �߰��� �̺�Ʈ�� ������ �ʿ��ϴٸ� �׳� ������Ʈ �ϳ��� ���� ������ �����Ҷ����� ����ϴ� �����ϸ� �̺�Ʈ �÷ο츦 Ȱ��ȭ �ϴ� ������ �ص� �ɵ��ϴ�.
    {
        if (stages[stageIndex].stageEvent != null)
            stages[stageIndex].stageEvent.SetActive(true);
    }
    private void StageEnd()//�ð��� �ٵǼ� endflow�� Ű�� �߸� �۵��� ������ �ִ� ������Ʈ�� �ϴ� �� ��Ȱ��ȭ�� ���� �׸��� �������� ��� ���� �� ���
    {
        movePeople.StageClear();
        for (int index = 0; index < 4; index++)
        {
            if (portals[index].gameObject.activeSelf)
                portals[index].EndStage();
            portals[index].gameObject.SetActive(false);
        }
        countPenalty += kickout.banPenalty;
        kickout.gameObject.SetActive(false);
        if(vipControl.activeSelf)
            vipControl.SetActive(false);
    }
}
