using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class VIP_Base : MonoBehaviour
{
    [SerializeField] private GameObject first;
    private SpriteRenderer destination;
    private float moveSpeed;
    public bool isFirst = true;
    public int portalIndex;
    public float MoveSpeed
    {
        set { moveSpeed = value; }
    }
    void Awake()
    {
        destination = transform.GetChild(0).GetComponent<SpriteRenderer>();
    }
    void OnEnable()
    {
        if (isFirst)
        {
            first.SetActive(true);
            isFirst = false;
        }
        do
        {
            portalIndex = Random.Range(0, GameManager.Instance.maxDestination);
        } while (!GameManager.Instance.portals[portalIndex].gameObject.activeSelf);
        destination.sprite = GameManager.Instance.areaSprites[GameManager.Instance.portals[portalIndex].AreaID];
    }
    public void Move(Vector3 position)
    {
        transform.DOMove(position, moveSpeed);
    }
    public abstract bool NecessaryCondition(Portal portal);//vip들의 진입 조건 확인
    public abstract void Process();//vip를 보냈을때 차원문의 인원수 패널티를 무마시키기위해 동작 보통 패널티만큼 게임매니저의 패널티 카운트를 미리 감소시켜놓음
}
