using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Volume_Image : MonoBehaviour
{
    [SerializeField] private Image image;
    [SerializeField] private Sprite[] sprites;

    void Awake()
    {
        image = GetComponent<Image>();
    }
    public void ChangeImage(float sliderValue)
    {
        if (0.002 < sliderValue && sliderValue <= 0.5)
            image.sprite = sprites[1];
        else if(sliderValue <= 0.002)
            image.sprite = sprites[2];
        else
            image.sprite = sprites[0];
    }
}
