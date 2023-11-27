using Redcode.Pools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class People_Control : MonoBehaviour
{
    [SerializeField] private List<People> people;
    [SerializeField] private List<Transform> movePoint;
    private PoolManager poolManager;

    void Awake()
    {
        poolManager = GetComponent<PoolManager>();
    }
    void Start()
    {
        for (int i = 0; i < 16; i++)
        {
            Spawn();
            people[i].transform.position = movePoint[i].position;
        }
    }
    public void Init()//각 스테이지 마다 다시 사람들을 배치할때 호출
    {
        for (int i = 0; i < 16; i++)
        {
            ReturnPool(people[i]);            
        }
        people.Clear();
        for (int i = 0; i < 16; i++)
        {
            Spawn();
            people[i].transform.position = movePoint[i].position;
        }
    }
    public void Next()
    {
        ReturnPool(people[0]);
        people.RemoveAt(0);        
        Spawn();
        people[0].Move(movePoint[0], 3.5f);
        for (int index = 1; index < 16; index++)
        {
            people[index].Move(movePoint[index], 1.5f);
        }
    }
    private void Spawn()
    {
        People newPeople = poolManager.GetFromPool<People>();
        people.Add(newPeople);        
    }
    private void ReturnPool(People clone)
    {
        poolManager.TakeToPool<People>(clone);
    }
}
