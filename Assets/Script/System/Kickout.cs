using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kickout : MonoBehaviour
{
    [SerializeField] private People_Control movePeople;
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
        if (auto)
        {
            if (collision.transform.GetComponent<People>().isBan)
                movePeople.Next();
        }
    }
    IEnumerator KickPeople()
    {
        while (true)
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                hit = Physics2D.Raycast(transform.position, Vector2.zero, 0, 6); // 마지막 위치로 사람이 이동하기 전에는 눌러도 반응하지 않게 하기위해 레이캐스트 이용
                if (hit)
                {
                    if (!hit.transform.GetComponent<People>().isBan)
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
