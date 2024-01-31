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
    IEnumerator BackGround()//�������� �����Ҷ� �̺�Ʈ�� �ִ°�� ����� �ٲ�� ���� ���缭 ���ڿ������� �̸� ��ħ���� �ٲٰ� �ڷ�ƾ���� �ѹ��� �����ϰ� �ٲ�
    {
        float time = GameManager.Instance.maxTime * 60 / 3;
        yield return new WaitForSeconds(time);
        Bg.sprite = BackGrounds[Random.Range(3, 6)];
        yield return new WaitForSeconds(time);
        Bg.sprite = BackGrounds[Random.Range(6, 8)];
    }
}
