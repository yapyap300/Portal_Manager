using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Redcode.Pools;
using DG.Tweening;

public class People : MonoBehaviour,IPoolObject
{
    private Vector3 init_pos;
    [SerializeField] private Animator myAnimator;
    [SerializeField] private RuntimeAnimatorController[] animations;
    [Header("# People Info")]
    public int area;
    public bool isBan;

    private void Awake()
    {
        init_pos = new Vector3(-9.5f, -1, 0);
        myAnimator = GetComponent<Animator>();
    }
    public void Move(Transform nextMovePosition,float moveSpeed)
    {
        myAnimator.SetBool("Walk", true);
        transform.DOMove(nextMovePosition.position, moveSpeed).SetEase(Ease.Linear).OnComplete(() => {
            myAnimator.SetBool("Walk", false);});
    }
    private void Init()//풀링시 각종 정보 랜덤으로 다시 갱신
    {
        transform.position = init_pos;
        area = Random.Range(0, GameManager.Instance.maxDestination);
        isBan = Random.Range(0,10) < 1;
        myAnimator.runtimeAnimatorController = animations[Random.Range(0,6)];        
    }
    public void OnCreatedInPool()
    {
        transform.position = init_pos;
    }
    public void OnGettingFromPool()
    {
        Init();
        if (Random.Range(0, 2) > 0)
        {
            myAnimator.SetBool("Wait", true);
        }
        else
            myAnimator.SetBool("Wait", false);
    }
}
