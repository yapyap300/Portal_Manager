using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    [Header("# Game Control")]
    [SerializeField] private GameObject endFlow;//스테이지 끝날때 활성화시킬 흐름
    [SerializeField] private Stage_Data[] stages;
    [SerializeField] private GameObject vipControl;
    public People_Control movePeople;
    public Portal[] portals;
    public Kickout kickout;
    public Sprite[] areaSprites;//각 구역의 문양을 순서대로 가지고 있어서 다른 곳에서 참조하게 한다.
    public int stageIndex;
    public float time;
    public int money;    
    public bool isStop;
    public int maxTime;
    [Header("# Stage Control")]
    public int afterNoon;
    public int night;
    public int maxDestination;//차원문의 갯수 스테이지마다 다른 갯수를 가질것
    public int vipIndex;//vip가 몇번째까지 등장했는지 구분할때 쓰는 변수
    public int maxCount;//차원문의 최대 정원
    [Header("# Stage End Control")]//돈 계산에 쓰는 변수들
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
    public void StageInit()//함수로 만들어두고 endflow에서 업그레이드 끝나고 실행시킬 예정
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
    public void StageEvent()//만들면서 많은 변화가 있었지만 결국 스테이지 초반에만 대화형식의 이벤트가 진행되는 이 게임의 한계상 이렇게만 해도 될듯하다 스테이지 중간에 이벤트나 연출이 필요하다면 그냥 오브젝트 하나를 만들어서 조건을 만족할때까지 대기하다 만족하면 이벤트 플로우를 활성화 하는 식으로 해도 될듯하다.
    {
        if (stages[stageIndex].stageEvent != null)
            stages[stageIndex].stageEvent.SetActive(true);
    }
    private void StageEnd()//시간이 다되서 endflow를 키면 잘못 작동될 위험이 있는 오브젝트들 일단 다 비활성화할 예정 그리고 스테이지 결과 받을 돈 계산
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
