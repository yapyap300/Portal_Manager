using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detect : MonoBehaviour
{
    [SerializeField] Sprite[] sprites;
    void OnTriggerEnter2D(Collider2D collision)//������ �ݶ��̴��� ������ �����͸� ����ϴ� ������Ʈ�� �ϳ����̶� ������ �ʿ䰡 ������
    {
        if (collision.GetComponent<People>().isBan)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = sprites[1];
            //���� �غ�Ǹ� �߼Ҹ� ���� ���� �ѹ� ���
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = sprites[0];
    }
}
