using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VIP_Control : MonoBehaviour
{
    [SerializeField] private LayerMask vipLayer;
    [SerializeField] private RaycastHit2D hit;
    [SerializeField] private List<VIP_Base> vip;
    private Vector3 entry = new Vector3(10, -4, 0);
    private Vector3 endTime = new Vector3(0, -7, 0);
    private int currentArea = -1;
    [SerializeField] private bool isMake = false;
    [SerializeField] private int coolTime = 0;
    public int defultWaitTime = 60;
    void OnEnable()
    {
        coolTime = defultWaitTime + GameManager.Instance.vipWaitTime * 5;
        StartCoroutine(Entry());
        StartCoroutine(Make());
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        currentArea = collision.GetComponent<VIP_Base>().portalIndex;
        StartCoroutine(Wait());
    }
    IEnumerator Wait()//vip���� ��ٸ��� �ð��� �־ �ʹ� �������θ� ������ �������� �������� �۵���Ű�� �г�Ƽ���� ������ �� �ް� �Ѵ�.
    {
        yield return new WaitForSeconds(coolTime);
        SoundManager.Instance.PlaySfx("Error");
        GameManager.Instance.portals[currentArea].WorkEntry();
        hit = Physics2D.Raycast(transform.position, Vector2.zero, 0, vipLayer);
        hit.transform.GetComponent<VIP_Base>().Move(endTime,false);
        isMake = false;
    }
    IEnumerator Entry()
    {
        while (true)
        {
            yield return null;
            if (Input.GetKeyDown(KeyCode.V))
            {
                hit = Physics2D.Raycast(transform.position, Vector2.zero, 0, vipLayer);
                if (hit)
                {
                    VIP_Base vip = hit.transform.GetComponent<VIP_Base>();
                    GameManager.Instance.portals[currentArea].WorkEntry();
                    if (vip.NecessaryCondition(GameManager.Instance.portals[currentArea]))
                    {
                        vip.Process();                        
                    }
                    else
                    {
                        SoundManager.Instance.PlaySfx("Error");
                        GameManager.Instance.bonus -= 2;//vip������ ��������Ű�� ������ ���ʽ��� 2���𿩼� 1000�� ���ظ� ���� ������ �������� �۵���Ű�鼭 �޴� �г�Ƽ���� ������ �ٹ���
                    }
                    vip.Move(entry,false);
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
            if(Random.Range(1,2) == 1)
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
