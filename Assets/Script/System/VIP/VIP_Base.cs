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
    public abstract bool NecessaryCondition(Portal portal);//vip���� ���� ���� Ȯ��
    public abstract void Process();//vip�� �������� �������� �ο��� �г�Ƽ�� ������Ű������ ���� ���� �г�Ƽ��ŭ ���ӸŴ����� �г�Ƽ ī��Ʈ�� �̸� ���ҽ��ѳ���
}
