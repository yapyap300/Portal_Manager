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
    public int destination;
    public bool isBan;

    private void Awake()
    {
        init_pos = new Vector3(-9.3f, -1, 0);
        mySprite = GetComponent<SpriteRenderer>();
        myAnimator = GetComponent<Animator>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Move(Transform nextMovePosition)
    {
        if(isFilp)
            mySprite.flipX = false;
        myAnimator.SetBool("Walk", true);
        gameObject.transform.DOMove(nextMovePosition.position, 1).OnComplete(() => { 
            if(isFilp)
                mySprite.flipX = true;
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
    private void Init()//풀링시 각종 정보 랜덤으로 다시 갱신
    {
        gameObject.transform.position = init_pos;
        destination = Random.Range(1, GameManager.Instance.maxDestination + 1);
        isBan = Random.Range(0,2) > 0? false: true;
        myAnimator.runtimeAnimatorController = animations[Random.Range(0,7)];
        StartCoroutine(AnimationStateChange());
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
