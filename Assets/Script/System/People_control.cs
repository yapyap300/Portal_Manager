using Redcode.Pools;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class People_control : MonoBehaviour
{
    [SerializeField] List<People> people;
    [SerializeField] List<Transform> move_Point;
    private PoolManager poolManager;

    void Awake()
    {
        poolManager = GetComponent<PoolManager>();
    }
    public void Init()//각 스테이지 마다 다시 사람들을 배치할때 호출
    {
        for (int i = 0; i < 16; i++)
        {
            ReturnPool(people[0]);
            people.RemoveAt(0);
        }
        for (int i = 0; i < 16; i++)
        {
            Spawn();
            people[^1].transform.position = move_Point[people.Count - 1].position;
        }
    }
    public void Next()
    {
        ReturnPool(people[0]);
        people.RemoveAt(0);        
        Spawn();
        people[0].Move(move_Point[0], 4.5f);
        for (int index = 0; index < people.Count; index++)
        {
            people[index].Move(move_Point[index],2.5f);
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
