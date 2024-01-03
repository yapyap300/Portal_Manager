using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VIP_Control : MonoBehaviour
{
    [SerializeField] private LayerMask vipLayer;
    [SerializeField] private RaycastHit2D hit;
    [SerializeField]private List<VIP_Base> vip;
    private int currentArea = -1;
    private bool isMake = false;
    void OnEnable()
    {
        StartCoroutine(Entry());
        StartCoroutine(Make());
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        currentArea = collision.GetComponent<VIP_Base>().portalIndex;
    }
    IEnumerator Entry()//���� ���� ��ȣ�� �´� Ű�ڵ带 �޾Ƽ� ť�� ����� ������ ��ȣ ����
    {
        while (true)
        {
            yield return null;
            if (Input.GetKeyDown(KeyCode.V))
            {
                hit = Physics2D.Raycast(transform.position, Vector2.zero, 0, vipLayer); // ������ ��ġ�� ����� �̵��ϱ� ������ ������ �������� �ʰ� �ϱ����� ����ĳ��Ʈ �̿�
                if (hit)
                {
                    VIP_Base vip = hit.transform.GetComponent<VIP_Base>();
                    GameManager.Instance.portals[currentArea].VipEntry();
                    if (vip.NecessaryCondition(GameManager.Instance.portals[currentArea]))
                    {
                        vip.Process();
                    }
                    else
                        GameManager.Instance.bonus -= 2;//vip������ ��������Ű�� ������ ���ʽ��� 2���𿩼� 1000�� ���ظ� ���� ������ �������� �۵���Ű�鼭 �޴� �г�Ƽ���� ������ �ٹ���
                    isMake = false;
                }
            }
        }
    }
    IEnumerator Make()
    {
        while (true)
        {
            yield return new WaitForSeconds(10f);
            if(Random.Range(1,101) == 1)
            {
                isMake = true;
                VIP_Base people = vip[Random.Range(0, GameManager.Instance.vipIndex)];
                people.gameObject.SetActive(true);
                people.Move(transform.position);
            }
            yield return new WaitUntil(() => !isMake);
        }
    }
}
