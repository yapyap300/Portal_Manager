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
    private int targetIndex = -1;
    private Coroutine waitCoroutine;
    [SerializeField] private bool isMake = false;
    [SerializeField] private int coolTime;
    [SerializeField] int defultWaitTime;
    [SerializeField] int nextTime;
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
        targetIndex = collision.GetComponent<VIP_Base>().portalIndex;
        waitCoroutine = StartCoroutine(Wait());
    }
    public void HurryUp()
    {
        coolTime /= 2;
        nextTime /= 2;
    }
    IEnumerator Wait()//vip마다 기다리는 시간이 있어서 너무 오래놔두면 강제로 목적지의 차원문을 작동시키고 패널티까지 고스란히 다 받게 한다.
    {
        Debug.Log($"wait {GameManager.Instance.time}");
        yield return new WaitForSeconds(coolTime);
        yield return new WaitUntil(() => !GameManager.Instance.portals[targetIndex].wait);
        SoundManager.Instance.PlaySfx("Error");
        GameManager.Instance.portals[targetIndex].WorkEntry();
        hit = Physics2D.Raycast(transform.position, Vector2.zero, 0, vipLayer);
        hit.transform.GetComponent<VIP_Base>().Move(entry,false);
        GameManager.Instance.bonus -= 1;
        yield return new WaitForSeconds(nextTime);
        isMake = false;
    }
    IEnumerator Entry()
    {
        while (true)
        {
            yield return null;
            if (Input.GetKeyDown(KeyCode.V) && !GameManager.Instance.portals[targetIndex].wait)
            {
                hit = Physics2D.Raycast(transform.position, Vector2.zero, 0, vipLayer);
                if (hit)
                {
                    VIP_Base vip = hit.transform.GetComponent<VIP_Base>();
                    if (vip.NecessaryCondition(GameManager.Instance.portals[targetIndex]))
                    {
                        vip.Process();
                        GameManager.Instance.bonus++;
                    }
                    else
                    {
                        SoundManager.Instance.PlaySfx("Error");
                        GameManager.Instance.bonus -= 1;
                    }
                    GameManager.Instance.portals[targetIndex].WorkEntry();
                    StopCoroutine(waitCoroutine);
                    vip.Move(entry,false);
                    yield return new WaitForSeconds(nextTime);
                    isMake = false;                    
                }
            }
        }
    }
    IEnumerator Make()
    {
        yield return new WaitForSeconds(30f);//처음부터 나오니까 너무 불합리하게 느껴져서 대기시간추가
        while (GameManager.Instance.time < GameManager.Instance.maxTime * 60f - 20) { //혹시 스테이지가 거의 끝나기 직전에 생성하면 vip의 첫번째 방문 이벤트가 무의미 해지는 경우가 생김
            yield return new WaitForSeconds(nextTime);
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
