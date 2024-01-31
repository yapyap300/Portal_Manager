using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGManager : MonoBehaviour
{
    [SerializeField] Sprite[] BackGrounds;
    [SerializeField] SpriteRenderer Bg;
    void Awake()
    {
        Bg = GetComponent<SpriteRenderer>();
    }
    void OnEnable()
    {
        Bg.sprite = BackGrounds[Random.Range(0, 4)];
        StartCoroutine(BackGround());
    }
    IEnumerator BackGround()//스테이지 시작할때 이벤트가 있는경우 배경이 바뀌기 전에 멈춰서 부자연스러움 미리 아침으로 바꾸고 코루틴으로 한번만 실행하게 바꿈
    {
        float time = GameManager.Instance.maxTime * 60 / 3;
        yield return new WaitForSeconds(time);
        Bg.sprite = BackGrounds[Random.Range(3, 6)];
        yield return new WaitForSeconds(time);
        Bg.sprite = BackGrounds[Random.Range(6, 8)];
    }
}
