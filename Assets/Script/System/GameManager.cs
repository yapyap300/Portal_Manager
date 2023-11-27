using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    [Header("# Game Control")]
    [SerializeField] private GameObject endFlow;//�������� ������ Ȱ��ȭ��ų �帧
    [SerializeField] private GameObject[] eventFlows;//�� ������������ �̺�Ʈ�� �߻���ų���ִ� �̺�Ʈ �帧�� ����ִ� �迭
    public Portal[] portals;
    public Sprite[] areaSprites;//�� ������ ������ ������� ������ �־ �ٸ� ������ �����ϰ� �Ѵ�.
    public int[] maxTime;
    public int stageIndex;
    public float time;
    public int money;    
    public bool isStop;
    [Header("# Stage Control")]
    public int afterNoon;
    public int night;
    public int maxDestination;//�������� ���� ���������� ������ �Ѱ��� ���� �÷���
    public int vipIndex;//vip�� ���°���� �����ߴ��� �����Ҷ� ���� ����
    public int maxCount;//�������� �ִ� ����
    [Header("# Stage End Control")]
    public int pay;
    public int countPenaltyMoney;
    public int areaPenaltyMoney;
    public int bonusMoney;
    [Header("# Upgrade Info")]
    public bool timeView;//���� ���������� �ð��� �� �� �ִ� ���׷��̵�
    public int countless;//������ ��������
    public bool isCountNumber;//������ ���ڷ� �����ֱ�
    public int waitTime;//������ �簡�� ��Ÿ��
    public int vipNumber;//VIP��� ��
    public bool autoBan;//�ڵ� ��
    public int vipColltime;//VIP �湮�ð� ����
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
        if(time >= maxTime[stageIndex] * 60f)
        {
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
    public void StageInit()
    {
        Stop();
        time = 0;
        afterNoon = maxTime[stageIndex] / 3;
        night = maxTime[stageIndex] / 3 * 2;
        Resume();
    }
    IEnumerator EventControl()
    {
        yield return new WaitUntil(() => !isStop);
    }
}
