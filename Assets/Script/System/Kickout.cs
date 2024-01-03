using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kickout : MonoBehaviour
{
    [SerializeField] private People_Control movePeople;
    [SerializeField] private LayerMask people;
    [SerializeField] private RaycastHit2D hit;
    public bool auto = false;
    public int banPenalty = 0;
    void OnEnable()
    {
        banPenalty = 0;
        StartCoroutine(KickPeople());
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
                    if (!currentUser.isBan && GameManager.Instance.portals[currentUser.area].gameObject.activeSelf)
                    {
                        SoundManager.Instance.PlaySfx("Bip");
                        banPenalty++;
                    }
                    movePeople.Next();
                }
            }
        }
    }

}
