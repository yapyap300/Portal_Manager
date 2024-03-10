using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Portal : MonoBehaviour
{
    [SerializeField] private LayerMask peopleLayer;
    [SerializeField] private KeyCode EntryCode1;//키패드의 숫자
    [SerializeField] private KeyCode EntryCode2;//위에 숫자키의 숫자 키패드가 없는 사람도 있을 수 있다.
    [SerializeField] private KeyCode WorkCode;
    [SerializeField] private RaycastHit2D hit;
    [SerializeField] private GameObject UI;
    [SerializeField] private Image peopleCount;
    [SerializeField] private Image waitUI;
    [SerializeField] private Text countText;
    public bool wait;
    [SerializeField] private float waitTime;
    [Header("Portal_Info")]
    [SerializeField] private int areaID;// 이벤트로 차원문이 담당하는 구역을 뒤바꾸기위해 필요한 변수
    [SerializeField] private int maxCount;// 점수 계산과 작동 관리를 위한 최대 정원
    [SerializeField] private int count;//스테이지 종료시 돈 계산을 위해 몇명의 사람을 보냈는지 세는 변수
    [SerializeField] private int countPenalty;// 정원에 맞춰 보내지 않으면 오르는 변수
    [SerializeField] private int differentPenalty;// 담당구역에 맞추지 못한 사람 수를 세는 변수
    public List<int> people = new(); // 차원문에 할당된 사람의 구역 번호를 가지고 있는 큐
    void OnEnable()
    {
        StartCoroutine(CountUI());
        StartCoroutine(Entry());
        StartCoroutine(Clear());
        if (GameManager.Instance.isCountNumber)
            StartCoroutine(CountNumberUI());        
    }
    public (KeyCode,KeyCode) Keys
    {
        get { return (EntryCode1, EntryCode2); }
    }
    public void WorkEntry()//vip관리에서도 접근할 수 있게 함수를 따로 뺌
    {
        wait = true;
        Work();
        SoundManager.Instance.PlaySfx("Work");
    }
    public void Init(int id)//스테이지마다 초기화를 이용 업그레이드나 이벤트 적용
    {
        areaID= id;
        count = 0;
        countPenalty = 0;
        differentPenalty= 0;
        maxCount = GameManager.Instance.maxCount;
        waitTime = 7f - GameManager.Instance.waitTime;
        people.Clear();
        wait = false;
    }
    public void EndStage()//스테이지 종료시 저장해둔 상태 변수들 전달하여 총 급여 계산에 사용
    {
        waitUI.transform.rotation = Quaternion.identity;
        waitUI.transform.DOKill();
        waitUI.gameObject.SetActive(false);
        wait = false;
        UI.SetActive(false);
        GameManager.Instance.count += count;
        GameManager.Instance.countPenalty += countPenalty;
        GameManager.Instance.areaPenalty += differentPenalty;
    }
    private void Work()//차원문이 작동할때마다 현재 상태에 따른 수치 변화 기록
    {
        count += people.Count;
        countPenalty += maxCount - people.Count;
        foreach(int number in people)
        {
            if (number != areaID)
                differentPenalty++;
        }
        people.Clear();
    }
    IEnumerator Entry()//각자 본인 번호에 맞는 키코드를 받아서 큐에 사람의 목적지 번호 삽입
    {
        while (true)
        {
            yield return null;
            if (!wait && (Input.GetKeyDown(EntryCode1) || Input.GetKeyDown(EntryCode2)))
            {
                hit = Physics2D.Raycast(transform.position, Vector2.zero, 0, peopleLayer); // 마지막 위치로 사람이 이동하기 전에는 눌러도 반응하지 않게 하기위해 레이캐스트 이용
                if (hit)
                {
                    GameManager.Instance.movePeople.Next();
                    if (people.Count == maxCount)
                    {
                        SoundManager.Instance.PlaySfx("Full");
                        peopleCount.DOColor(Color.black, 0.16f).SetLoops(3).OnComplete(() => peopleCount.color = Color.white);
                        countPenalty++;
                    }
                    else
                    {
                        if (hit.transform.GetComponent<People>().isBan)
                            countPenalty++;
                        GameManager.Instance.defultPay += 5;
                        people.Add(hit.transform.GetComponent<People>().area);
                        SoundManager.Instance.PlaySfx("Entry");                        
                    }                    
                }
            }
        }
    }
    IEnumerator Clear()//최소 한명이 들어있을때 키를 누르면 현재 상태에 따라 계산하는 함수 호출
    {
        while (true)
        {
            if (wait)
            {
                waitUI.gameObject.SetActive(true);
                waitUI.transform.DORotate(new Vector3(0f, 0f, 360f), 2f, RotateMode.FastBeyond360).SetLoops(-1).SetEase(Ease.Linear);
                yield return new WaitForSeconds(waitTime);
                waitUI.transform.rotation = Quaternion.identity;
                waitUI.transform.DOKill();
                waitUI.gameObject.SetActive(false);
                wait = false;
            }
            yield return null;
            if (0 < people.Count && Input.GetKeyDown(WorkCode))
            {
                WorkEntry();
            }
        }
    }
    IEnumerator CountUI()
    {
        UI.SetActive(true);
        while (true)
        {
            yield return null;
            peopleCount.fillAmount = (float)people.Count / maxCount;
        }
    }
    IEnumerator CountNumberUI()
    {
        countText.gameObject.SetActive(true);
        while (true)
        {
            yield return null;
            countText.text = $"{people.Count} / {maxCount}";
        }
    }
}
