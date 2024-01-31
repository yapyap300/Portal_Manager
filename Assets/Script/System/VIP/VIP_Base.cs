using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class VIP_Base : MonoBehaviour
{
    [SerializeField] private GameObject first;//첫 등장일때 이벤트 발동 오브젝트 
    private Vector3 defult = Vector3.zero;
    [SerializeField] private SpriteRenderer destination;//목적지 구역 이미지
    [SerializeField] private Animator animator;
    [SerializeField] private float moveSpeed;    
    public bool isFirst = true;
    public int portalIndex;
    protected virtual void OnEnable()
    {
        do
        {
            portalIndex = Random.Range(0, GameManager.Instance.maxDestination);
        } while (!GameManager.Instance.portals[portalIndex].gameObject.activeSelf);
        destination.sprite = GameManager.Instance.areaSprites[GameManager.Instance.portalArea[portalIndex]];
    }
    void OnDisable()
    {
        transform.position = defult;
    }
    public void Move(Vector3 position,bool active = true)
    {
        animator.SetBool("Walk",true);
        transform.DOMove(position, moveSpeed).SetEase(Ease.Linear)
            .OnComplete(() => { animator.SetBool("Walk", false); if (isFirst) { first.SetActive(true); isFirst = false; } gameObject.SetActive(active); });
    }

    public abstract bool NecessaryCondition(Portal portal);//vip들의 진입 조건 확인
    public abstract void Process();//vip를 보냈을때 차원문의 인원수 패널티를 무마시키기위해 동작 보통 패널티만큼 게임매니저의 패널티 카운트를 미리 감소시켜놓음
}
