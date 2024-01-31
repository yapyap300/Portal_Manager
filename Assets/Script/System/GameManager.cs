using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    [Header("# Game Control")]
    [SerializeField] private Material mixBackground;
    [SerializeField] private GameObject endFlow;//스테이지 끝날때 활성화시킬 흐름
    [SerializeField] private Stage_Data[] stages;
    [SerializeField] private GameObject vipControl;
    [SerializeField] private GameObject backgroundControl;    
    public People_Control movePeople;
    public Portal[] portals;
    public Kickout kickout;
    public Sprite[] areaSprites;//각 구역의 문양을 순서대로 가지고 있어서 다른 곳에서 참조하게 한다.
    public int[] portaToArea;
    public int stageIndex;
    public float time; 
    public bool isStop;
    public int maxTime;
    [Header("# Stage Event Info")]
    public bool isMix;
    public bool isInactive;
    public int InactiveIndex;
    [Header("# Stage Control")]
    public int[] portalArea;
    public int maxDestination;//차원문의 갯수 스테이지마다 다른 갯수를 가질것
    public int vipIndex;//vip가 몇번째까지 등장했는지 구분할때 쓰는 변수
    public int maxCount;//차원문의 최대 정원
    [Header("# Money Control")]//돈 계산에 쓰는 변수들
    public int money;
    public int endMoney;
    public int defultPay;
    public int count;
    public int countPenalty;
    public int areaPenalty;
    public int bonus;
    [Header("# Upgrade Info")]
    public bool timeView;//현재 스테이지의 시간을 볼 수 있는 업그레이드
    public int countless;//차원문 정원감소
    public bool isCountNumber;//차원문 숫자로 보여주기
    public int waitTime;//차원문 재가동 쿨타임
    public bool autoBan;//자동 밴
    public bool areaView;//구역 표시 안 키면 각 차원문이 무슨 구역을 담당하는지 확인불가 뒤틀림때 유용함
    public int vipWaitTime;//VIP의 대기시간 증가
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
        }
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
    public void StageInit()//함수로 만들어두고 endflow에서 업그레이드 끝나고 실행시킬 예정
    {
        isMix = stages[stageIndex].isMix;
        if (isMix)
            RandomArea();
        isInactive = false;
        stageIndex++;
        maxDestination = stages[stageIndex].portalCount;
        maxTime = stages[stageIndex].stageTime;
        maxCount = 25 - (countless * 3);
        vipIndex = stages[stageIndex].vipIndex;
        kickout.auto = autoBan;
        for (int index = 0; index < maxDestination; index++)
        {
            portals[index].Init(portalArea[index]);
            if (!stages[stageIndex].portalActive[index])
            {
                isInactive = true;
                InactiveIndex = index;
            }
            else
            {
                portals[index].gameObject.SetActive(true);
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
        movePeople.Init();
        if (isMix)
        {
            backgroundControl.GetComponent<SpriteRenderer>().material = mixBackground;
            SoundManager.Instance.PlaySfx("Mix");
        }
        backgroundControl.SetActive(true);
        SoundManager.Instance.SetBGM(stageIndex % 3);
        if (stageIndex == 1)
            SoundManager.Instance.PlayBGM();
    }
    public void StageEvent()//만들면서 많은 변화가 있었지만 결국 스테이지 초반에만 대화형식의 이벤트가 진행되는 이 게임의 한계상 이렇게만 해도 될듯하다 스테이지 중간에 이벤트나 연출이 필요하다면 그냥 오브젝트 하나를 만들어서 조건을 만족할때까지 대기하다 만족하면 이벤트 플로우를 활성화 하는 식으로 해도 될듯하다.
    {
        if (stages[stageIndex].stageEvent != null)
            stages[stageIndex].stageEvent.SetActive(true);
    }
    public void StageEnd()//시간이 다되서 endflow를 키면 잘못 작동될 위험이 있는 오브젝트들 일단 다 비활성화할 예정 그리고 스테이지 결과 받을 돈 계산
    {
        movePeople.StageClear();
        for (int index = 0; index < 4; index++)
        {
            if (portals[index].gameObject.activeSelf)
                portals[index].EndStage();
            portals[index].gameObject.SetActive(false);
            portalArea[index] = index;
        }
        countPenalty += kickout.banPenalty;
        kickout.gameObject.SetActive(false);
        if(vipControl.activeSelf)
            vipControl.SetActive(false);
        backgroundControl.SetActive(false);
    }
    public void RandomArea()//스테이지 인덱스 랜덤
    {
        bool[] check = new bool[4];
        for (int index = 0; index < maxDestination; index++)
        {
            int area;
            do
            {
                area = Random.Range(0, maxDestination);
            } while (area == index || check[area]);
            check[area] = true;
            portalArea[index] = area;
            portaToArea[area] = index;//각 차원문에 담당 구역을 전해줫다면 각 구역에 연결된 차원문의 인덱스도 따로 저장 이유는 뒤섞임과 비활성화가 같이 됐을때 이용하려고            
        }
    }
}
