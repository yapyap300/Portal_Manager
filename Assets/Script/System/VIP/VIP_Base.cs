using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class VIP_Base : MonoBehaviour
{
    [SerializeField] private GameObject first;//ù �����϶� �̺�Ʈ �ߵ� ������Ʈ 
    private Vector3 defult = Vector3.zero;
    [SerializeField] private SpriteRenderer destination;//������ ���� �̹���
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

    public abstract bool NecessaryCondition(Portal portal);//vip���� ���� ���� Ȯ��
    public abstract void Process();//vip�� �������� �������� �ο��� �г�Ƽ�� ������Ű������ ���� ���� �г�Ƽ��ŭ ���ӸŴ����� �г�Ƽ ī��Ʈ�� �̸� ���ҽ��ѳ���
}
