using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Detect_Area : MonoBehaviour
{
    [SerializeField] Image area;
    private Color fade = new(1, 1, 1, 0);
    void OnTriggerEnter2D(Collider2D collision)
    {
        area.color = Color.white;
        area.sprite = GameManager.Instance.areaSprites[collision.GetComponent<People>().area];
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        area.color = fade;
    }
}
