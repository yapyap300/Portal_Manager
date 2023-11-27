using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Detect_Area : MonoBehaviour
{
    [SerializeField] Image area;
    
    void OnTriggerEnter2D(Collider2D collision)
    {        
        area.sprite = GameManager.Instance.areaSprites[collision.GetComponent<People>().area];
    }
}
