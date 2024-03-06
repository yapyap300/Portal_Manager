using DG.Tweening;
using Redcode.Pools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class People_Control : MonoBehaviour
{
    [SerializeField] private List<People> people;
    [SerializeField] private Transform[] movePoint;
    private PoolManager poolManager;

    void Awake()
    {
        poolManager = GetComponent<PoolManager>();
    }
    public void StageClear()//스테이지 1일때도 init을 부르다보니 여기서 오류가 나서 따로 분리
    {
        for (int index = 0; index < people.Count; index++)
        {
            ReturnPool(people[index]);
        }
        people.Clear();
    }
    public void Init()//각 스테이지 마다 다시 사람들을 배치할때 호출
    {
        for (int i = 0; i < 16; i++)
        {
            Spawn();
            people[i].transform.position = movePoint[i].position;
        }
        if (people[0].isBan)
            people[0].isBan = false;
    }
    public void Next()
    {
        Spawn();
        ReturnPool(people[0]);
        people.RemoveAt(0);
        people[0].Move(movePoint[0], 1f);
        for (int index = 1; index < 16; index++)
        {
            people[index].Move(movePoint[index], 0.5f);
        }
    }
    private void Spawn()
    {
        People newPeople = poolManager.GetFromPool<People>();
        people.Add(newPeople);        
    }
    private void ReturnPool(People clone)
    {
        clone.transform.DOKill();
        poolManager.TakeToPool<People>(clone);
    }
}
