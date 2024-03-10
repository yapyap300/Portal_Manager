using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VIP_Control : MonoBehaviour
{
    [SerializeField] private LayerMask vipLayer;
    [SerializeField] private RaycastHit2D hit;
    [SerializeField] private List<VIP_Base> vip;
    private Vector3 entry = new Vector3(10, -4, 0);
    private int currentArea = -1;
    private Coroutine waitCoroutine;
    [SerializeField] private bool isMake = false;
    [SerializeField] private int coolTime = 0;
    public int defultWaitTime = 35;
    void OnEnable()
    {
        isMake = false;
        coolTime = defultWaitTime + GameManager.Instance.vipWaitTime * 5;
        StartCoroutine(Entry());
        StartCoroutine(Make());
    }
    void OnDisable()
    {
        foreach(VIP_Base chracter in vip)
        {
            if(chracter != null)
                chracter.gameObject.SetActive(false);
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        currentArea = collision.GetComponent<VIP_Base>().portalIndex;
        waitCoroutine = StartCoroutine(Wait());
    }
    IEnumerator Wait()//vip마다 기다리는 시간이 있어서 너무 오래놔두면 강제로 목적지의 차원문을 작동시키고 패널티까지 고스란히 다 받게 한다.
    {
        Debug.Log($"wait {GameManager.Instance.time}");
        yield return new WaitForSeconds(coolTime);
        SoundManager.Instance.PlaySfx("Error");
        GameManager.Instance.portals[currentArea].WorkEntry();
        hit = Physics2D.Raycast(transform.position, Vector2.zero, 0, vipLayer);
        hit.transform.GetComponent<VIP_Base>().Move(entry,false);
        GameManager.Instance.bonus -= 2;
        isMake = false;
    }
    IEnumerator Entry()
    {
        while (true)
        {
            yield return null;
            if (Input.GetKeyDown(KeyCode.V) && !GameManager.Instance.portals[currentArea].wait)
            {
                hit = Physics2D.Raycast(transform.position, Vector2.zero, 0, vipLayer);
                if (hit)
                {
                    VIP_Base vip = hit.transform.GetComponent<VIP_Base>();
                    GameManager.Instance.portals[currentArea].WorkEntry();
                    if (vip.NecessaryCondition(GameManager.Instance.portals[currentArea]))
                    {
                        vip.Process();
                        GameManager.Instance.bonus++;
                    }
                    else
                    {
                        SoundManager.Instance.PlaySfx("Error");
                        GameManager.Instance.bonus -= 2;//vip조건을 만족못시키고 보내면 보너스가 2가깎여서 1000원 손해를 보고 억지로 차원문을 작동시키면서 받는 패널티까지 고스란히 다받음
                    }
                    vip.Move(entry,false);
                    isMake = false;
                    StopCoroutine(waitCoroutine);
                }
            }
        }
    }
    IEnumerator Make()
    {
        while (GameManager.Instance.time < GameManager.Instance.maxTime * 60f - 20) { //혹시 스테이지가 거의 끝나기 직전에 생성하면 vip의 첫번째 방문 이벤트가 무의미 해지는 경우가 생김
            yield return new WaitForSeconds(10f);
            int number = Random.Range(0, 100);
            Debug.Log(number);
            if(number < 20)
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
