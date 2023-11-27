using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    [Header("# Game Control")]
    [SerializeField] private GameObject endFlow;//스테이지 끝날때 활성화시킬 흐름
    [SerializeField] private GameObject[] eventFlows;//각 스테이지마다 이벤트를 발생시킬수있는 이벤트 흐름이 들어있는 배열
    public Portal[] portals;
    public Sprite[] areaSprites;//각 구역의 문양을 순서대로 가지고 있어서 다른 곳에서 참조하게 한다.
    public int[] maxTime;
    public int stageIndex;
    public float time;
    public int money;    
    public bool isStop;
    [Header("# Stage Control")]
    public int afterNoon;
    public int night;
    public int maxDestination;//차원문의 갯수 스테이지가 지날때 한개씩 점차 늘려감
    public int vipIndex;//vip가 몇번째까지 등장했는지 구분할때 쓰는 변수
    public int maxCount;//차원문의 최대 정원
    [Header("# Stage End Control")]
    public int pay;
    public int countPenaltyMoney;
    public int areaPenaltyMoney;
    public int bonusMoney;
    [Header("# Upgrade Info")]
    public bool timeView;//현재 스테이지의 시간을 볼 수 있는 업그레이드
    public int countless;//차원문 정원감소
    public bool isCountNumber;//차원문 숫자로 보여주기
    public int waitTime;//차원문 재가동 쿨타임
    public int vipNumber;//VIP명단 수
    public bool autoBan;//자동 밴
    public int vipColltime;//VIP 방문시간 감소
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
