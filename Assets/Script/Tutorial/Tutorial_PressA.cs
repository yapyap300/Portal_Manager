using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial_PressA : Tutorial_Base
{
    [SerializeField] Image[] setting;
    [SerializeField] Image count;// 1�� ���� ǥ�� ���� ui
    [SerializeField] AudioSource source;
    [SerializeField] AudioClip sound;
    bool isEnd = false;
    public override void Enter()
    {
        StartCoroutine(PressA());
    }

    public override void Excute(Tutorial_Control manager)
    {
        if (isEnd)
            manager.SetNextTutorial();
    }

    public override void Exit()
    {
    }
    IEnumerator PressA()
    {
        setting[0].gameObject.SetActive(true);
        setting[1].gameObject.SetActive(true);
        while (!Input.GetKeyDown(KeyCode.A))
        {
            yield return null;
        }
        setting[0].gameObject.SetActive(false);
        setting[1].gameObject.SetActive(false);

        source.clip = sound;
        source.Play();
        count.fillAmount = 0f;        

        isEnd = true;
    }
}
