using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Detect : MonoBehaviour
{
    [SerializeField] Sprite[] sprites;
    void OnTriggerEnter2D(Collider2D collision)//어차피 콜라이더를 가지고 디텍터를 통과하는 오브젝트는 하나뿐이라 구별할 필요가 없을듯
    {
        if (collision.GetComponent<People>().isBan)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = sprites[1];
            //사운드 준비되면 삐소리 나는 사운드 한번 재생
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = sprites[0];
    }
}
