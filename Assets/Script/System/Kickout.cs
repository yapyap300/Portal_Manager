using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kickout : MonoBehaviour
{
    [SerializeField] private People_Control movePeople;
    [SerializeField] private LayerMask people;
    [SerializeField] private RaycastHit2D hit;
    private KeyCode entryCode1;
    private KeyCode entryCode2;
    public bool auto = false;
    public int banPenalty = 0;
    void OnEnable()
    {
        banPenalty = 0;
        StartCoroutine(KickPeople());
        if (GameManager.Instance.isInactive)//��Ż�� �ϳ� ��Ȱ��ȭ�Ǹ� �� ��Ż�� �ȳ��ϴ°� �� ��ũ��Ʈ���� �޾Ƽ� ��������
        {
            var portalKeyCode = GameManager.Instance.portals[GameManager.Instance.InactiveIndex].Keys;
            entryCode1 = portalKeyCode.Item1;
            entryCode2 = portalKeyCode.Item2;
            StartCoroutine(InactivePortal());
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (!GameManager.Instance.isStop && auto)
        {
            if (collision.transform.GetComponent<People>().isBan)
                movePeople.Next();
        }
    }
    IEnumerator KickPeople()
    {
        while (true)
        {
            yield return null;
            if (Input.GetKeyDown(KeyCode.X))
            {
                hit = Physics2D.Raycast(transform.position, Vector2.zero, 0, people); // ������ ��ġ�� ����� �̵��ϱ� ������ ������ �������� �ʰ� �ϱ����� ����ĳ��Ʈ �̿�
                if (hit)
                {
                    People currentUser = hit.transform.GetComponent<People>();
                    if (!currentUser.isBan && GameManager.Instance.portals[GameManager.Instance.portalToArea[currentUser.area]].gameObject.activeSelf)//������ ����� �������� �ε����� �߰��Ͽ� �ڼ��Ӱ� ��Ȱ�� �̺�Ʈ�� ���� �����ϴ�.
                    {
                        SoundManager.Instance.PlaySfx("Error");
                        banPenalty++;
                    }
                    movePeople.Next();
                }
            }
        }
    }
    IEnumerator InactivePortal()
    {
        while (true)
        {
            yield return null;
            if (Input.GetKeyDown(entryCode1) || Input.GetKeyDown(entryCode2))
            {
                hit = Physics2D.Raycast(transform.position, Vector2.zero, 0, people);
                if (hit)
                {
                    SoundManager.Instance.PlaySfx("Error");
                    banPenalty++;
                    
                    movePeople.Next();
                }
            }
        }
    }

}
