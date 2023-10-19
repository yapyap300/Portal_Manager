using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Portal : MonoBehaviour
{
    [SerializeField] private Image peopleCount;
    [SerializeField] private Text countText;
    [Header("Portal_Info")]
    [SerializeField] private int areaID = 0;
    [SerializeField] private int maxCount = 30;
    [SerializeField] private int count = 0;
    [SerializeField] private int countPenalty = 0;
    [SerializeField] private int differentPenalty = 0;
    public Queue<int> people = new Queue<int>(); // 차원문에 할당된 사람의 구역 번호를 가지고 있는 큐
    private void Start()
    {
        StartCoroutine(CountNumberUI());
        StartCoroutine(CountUI());
    }
    public void Init(int id)
    {
        areaID= id;
        count = 0;
        countPenalty = 0;
        differentPenalty= 0;
        maxCount = GameManager.Instance.maxCount;
        people.Clear();
        if(GameManager.Instance.isCountNumber)
            countText.gameObject.SetActive(true);
    }
    public void Work()
    {
        count += people.Count;
        countPenalty += Mathf.Abs(maxCount - people.Count);
        while(people.Count > 0)
        {
            if (people.Dequeue() != areaID)
                differentPenalty++;
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
