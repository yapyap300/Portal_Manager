using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    [SerializeField] private GameObject gameOverSprite;
    [SerializeField] private Button nextButton;
    [SerializeField] private Text returnText;

    void OnEnable()
    {
        DOVirtual.DelayedCall(3f, () => nextButton.interactable = true);
        returnText.DOFade(0, 1f).SetLoops(-1, LoopType.Yoyo).SetUpdate(true);
        gameOverSprite.SetActive(true);
    }
}
