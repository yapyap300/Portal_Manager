using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Redcode.Pools;
using DG.Tweening;

public class People : MonoBehaviour,IPoolObject
{
    private Vector3 init_pos;    
    [SerializeField] private SpriteRenderer mySprite;
    [SerializeField] private Animator myAnimator;
    [SerializeField] private RuntimeAnimatorController[] animations;
    [SerializeField] private bool isFilp;
    [Header("# People Info")]
    public int area;
    public bool isBan;

    private void Awake()
    {
        init_pos = new Vector3(-9.3f, -1, 0);
        mySprite = GetComponent<SpriteRenderer>();
        myAnimator = GetComponent<Animator>();
    }
    public void Move(Transform nextMovePosition,float moveSpeed)
    {
        if(isFilp)
            mySprite.flipX = false;
        myAnimator.SetBool("Walk", true);
        gameObject.transform.DOMove(nextMovePosition.position, moveSpeed).SetEase(Ease.Linear).OnComplete(() => {
            myAnimator.SetBool("Walk", false);
        });
    }
    IEnumerator AnimationStateChange()
    {
        if (myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            int rand = Random.Range(0, 3);
            switch(rand)
            {
                case 0:
                    myAnimator.SetTrigger("Wait");
                    break;
                case 1:
                    myAnimator.SetTrigger("Talk");
                    break;                
            }
        }
        yield return new WaitForSeconds(5f);
    }
    IEnumerator Filp()
    {
        yield return new WaitForSeconds(60f);
        mySprite.flipX = !mySprite.flipX;
    }
    private void Init()//풀링시 각종 정보 랜덤으로 다시 갱신
    {
        gameObject.transform.position = init_pos;
        area = Random.Range(0, GameManager.Instance.maxDestination);
        isBan = Random.Range(0,2) > 0;
        myAnimator.runtimeAnimatorController = animations[Random.Range(0,6)];
        StartCoroutine(AnimationStateChange());
        if(isFilp)
            StartCoroutine(Filp());
    }
    public void OnCreatedInPool()
    {
        if (Random.Range(0, 2) > 0)
        {
            isFilp = true;
            mySprite.flipX = true;
        }
    }
    public void OnGettingFromPool()
    {
        Init();
    }
}
