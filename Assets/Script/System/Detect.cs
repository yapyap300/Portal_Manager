using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detect : MonoBehaviour
{
    [SerializeField] Sprite[] sprites;
    private AudioSource bip;
    void Awake()
    {
        bip= GetComponent<AudioSource>();
    }
    void OnTriggerEnter2D(Collider2D collision)//������ �ݶ��̴��� ������ �����͸� ����ϴ� ������Ʈ�� �ϳ����̶� ������ �ʿ䰡 ������
    {
        if (collision.GetComponent<People>().isBan)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = sprites[1];
            bip.Play();
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = sprites[0];
    }
}
