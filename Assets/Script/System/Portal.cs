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
    [SerializeField] private KeyCode EntryCode1;//Ű�е��� ����
    [SerializeField] private KeyCode EntryCode2;//���� ����Ű�� ���� Ű�е尡 ���� ����� ���� �� �ִ�.
    [SerializeField] private KeyCode WorkCode;
    [SerializeField] private RaycastHit2D hit;
    [SerializeField] private GameObject UI;
    [SerializeField] private Image peopleCount;
    [SerializeField] private Image waitUI;
    [SerializeField] private Text countText;
    public bool wait;
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
    public void WorkEntry()//vip���������� ������ �� �ְ� �Լ��� ���� ��
    {
        wait = true;
        Work();
        SoundManager.Instance.PlaySfx("Work");
    }
    public void Init(int id)//������������ �ʱ�ȭ�� �̿� ���׷��̵峪 �̺�Ʈ ����
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
    public void EndStage()//�������� ����� �����ص� ���� ������ �����Ͽ� �� �޿� ��꿡 ���
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
            if (!wait && (Input.GetKeyDown(EntryCode1) || Input.GetKeyDown(EntryCode2)))
            {
                hit = Physics2D.Raycast(transform.position, Vector2.zero, 0, peopleLayer); // ������ ��ġ�� ����� �̵��ϱ� ������ ������ �������� �ʰ� �ϱ����� ����ĳ��Ʈ �̿�
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
    IEnumerator Clear()//�ּ� �Ѹ��� ��������� Ű�� ������ ���� ���¿� ���� ����ϴ� �Լ� ȣ��
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
