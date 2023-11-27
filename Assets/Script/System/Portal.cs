using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class Portal : MonoBehaviour
{
    [SerializeField] private KeyCode EntryCode;
    [SerializeField] KeyCode WorkCode;
    [SerializeField] private People_Control movePeople;
    [SerializeField] private RaycastHit2D hit;
    [SerializeField] private Image peopleCount;
    [SerializeField] private Image waitUI;
    [SerializeField] private Text countText;
    [SerializeField] private bool wait;
    [SerializeField] private float waitTime;
    [Header("Portal_Info")]
    [SerializeField] private int areaID;// �̺�Ʈ�� �������� ����ϴ� ������ �ڹٲٱ����� �ʿ��� ����
    [SerializeField] private int maxCount;// ���� ���� �۵� ������ ���� �ִ� ����
    [SerializeField] private int count;//�������� ����� �� ����� ���� ����� ����� ���´��� ���� ����
    [SerializeField] private int countPenalty;// ������ ���� ������ ������ ������ ����
    [SerializeField] private int differentPenalty;// ��籸���� ������ ���� ��� ���� ���� ����
    public List<int> people = new(); // �������� �Ҵ�� ����� ���� ��ȣ�� ������ �ִ� ť
    void OnEnable()
    {
        StartCoroutine(CountNumberUI());
        StartCoroutine(CountUI());
        StartCoroutine(Entry());
        StartCoroutine(Clear());
    }
    public int GetAreaID()
    {
        return areaID;
    }
    public void Init(int id)//������������ �ʱ�ȭ�� �̿� ���׷��̵峪 �̺�Ʈ ����
    {
        areaID= id;
        count = 0;
        countPenalty = 0;
        differentPenalty= 0;
        maxCount = GameManager.Instance.maxCount;
        waitTime = 5 - GameManager.Instance.waitTime;
        people.Clear();
        if(GameManager.Instance.isCountNumber)
            countText.gameObject.SetActive(true);
    }
    public void EndStage()
    {

    }
    private void Work()//�������� �۵��Ҷ����� ���� ���¿� ���� ��ġ ��ȭ ���
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
    IEnumerator Entry()//���� ���� ��ȣ�� �´� Ű�ڵ带 �޾Ƽ� ť�� ����� ������ ��ȣ ����
    {
        while (true)
        {
            yield return null;
            if (Input.GetKeyDown(EntryCode))
            {
                hit = Physics2D.Raycast(transform.position, Vector2.zero, 0, 6); // ������ ��ġ�� ����� �̵��ϱ� ������ ������ �������� �ʰ� �ϱ����� ����ĳ��Ʈ �̿�
                if (hit)
                {
                    if (people.Count == maxCount)
                    {
                        SoundManager.Instance.PlaySfx("Bip");
                        peopleCount.DOColor(Color.black, 0.5f).SetLoops(3).OnComplete(() => peopleCount.color = Color.white);
                        countPenalty++;
                    }
                    else
                    {
                        people.Add(hit.transform.GetComponent<People>().area);
                        SoundManager.Instance.PlaySfx("Entry");                        
                    }
                    movePeople.Next();
                }
            }
        }
    }
    IEnumerator Clear()//�ּ� �Ѹ��� ��������� Ű�� ������ ���� ���¿� ���� ����ϴ� �Լ� ȣ��
    {
        while (true)
        {
            if (wait)
            {
                yield return new WaitForSeconds(waitTime);
                waitUI.DOKill();
                waitUI.gameObject.SetActive(false);
                wait = false;
            }
            yield return null;
            if (0 < people.Count && Input.GetKeyDown(WorkCode))
            {
                wait = true;
                waitUI.gameObject.SetActive(true);
                waitUI.transform.DORotate(new Vector3(0f, 0f, 360f), 2f,RotateMode.FastBeyond360).SetLoops(-1).SetEase(Ease.Linear);
                Work();
                SoundManager.Instance.PlaySfx("Work");
            }
        }
    }
    IEnumerator CountUI()
    {
        yield return null;
        peopleCount.fillAmount = people.Count / maxCount;        
    }
    IEnumerator CountNumberUI()
    {
        yield return null;
        countText.text = $"{people.Count} / {maxCount}";
        if(people.Count / maxCount > 1)
            countText.color = Color.red;
        else
            countText.color = Color.white;
    }
}
