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
        if (GameManager.Instance.isInactive)//포탈이 하나 비활성화되면 그 포탈로 안내하는걸 이 스크립트에서 받아서 반응해줌
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
                hit = Physics2D.Raycast(transform.position, Vector2.zero, 0, people); // 마지막 위치로 사람이 이동하기 전에는 눌러도 반응하지 않게 하기위해 레이캐스트 이용
                if (hit)
                {
                    People currentUser = hit.transform.GetComponent<People>();
                    if (!currentUser.isBan && GameManager.Instance.portals[GameManager.Instance.portalToArea[currentUser.area]].gameObject.activeSelf)//구역과 연결된 차원문의 인덱싱을 추가하여 뒤섞임과 비활성 이벤트도 같이 가능하다.
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
