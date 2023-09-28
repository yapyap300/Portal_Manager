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

    public void Next()
    {
        ReturnPool(people[0]);
        people.RemoveAt(0);
        Spawn();
        for(int index = 0; index < people.Count; index++)
        {
            people[index].Move(move_Point[index]);
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
