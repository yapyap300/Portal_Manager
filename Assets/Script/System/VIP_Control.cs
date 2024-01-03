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
    IEnumerator Entry()//각자 본인 번호에 맞는 키코드를 받아서 큐에 사람의 목적지 번호 삽입
    {
        while (true)
        {
            yield return null;
            if (Input.GetKeyDown(KeyCode.V))
            {
                hit = Physics2D.Raycast(transform.position, Vector2.zero, 0, vipLayer); // 마지막 위치로 사람이 이동하기 전에는 눌러도 반응하지 않게 하기위해 레이캐스트 이용
                if (hit)
                {
                    VIP_Base vip = hit.transform.GetComponent<VIP_Base>();
                    GameManager.Instance.portals[currentArea].VipEntry();
                    if (vip.NecessaryCondition(GameManager.Instance.portals[currentArea]))
                    {
                        vip.Process();
                    }
                    else
                        GameManager.Instance.bonus -= 2;//vip조건을 만족못시키고 보내면 보너스가 2가깎여서 1000원 손해를 보고 억지로 차원문을 작동시키면서 받는 패널티까지 고스란히 다받음
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
