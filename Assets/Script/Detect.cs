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
    void OnTriggerEnter2D(Collider2D collision)//어차피 콜라이더를 가지고 디텍터를 통과하는 오브젝트는 하나뿐이라 구별할 필요가 없을듯
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
