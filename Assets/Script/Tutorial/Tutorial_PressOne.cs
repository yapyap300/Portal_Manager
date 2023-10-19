using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial_PressOne : Tutorial_Base
{
    [SerializeField] Image[] setting;
    [SerializeField] Image count;// 1명 들어간걸 표시 해줄 ui
    [SerializeField] List<People> people;
    [SerializeField] List<Transform> move_Point;
    [SerializeField] AudioSource source;
    [SerializeField] AudioClip sound;
    bool isEnd = false;
    public override void Enter()
    {
        StartCoroutine(PressOne());
    }

    public override void Excute(Tutorial_Control manager)
    {
        if (isEnd)
            manager.SetNextTutorial();
    }

    public override void Exit()
    {        
    }
    IEnumerator PressOne()
    {
        setting[0].gameObject.SetActive(true);
        setting[1].gameObject.SetActive(true);

        while (!Input.GetKeyDown(KeyCode.Alpha1) && !Input.GetKeyDown(KeyCode.Keypad1))
        {
            yield return null;
        }

        setting[0].gameObject.SetActive(false);
        setting[1].gameObject.SetActive(false);
        source.clip = sound;
        source.Play();
        count.fillAmount = 0.03f;
        Next();

        isEnd = true;
    }
    private void Next()
    {
        people[0].gameObject.SetActive(false);
        people.RemoveAt(0);
        people[0].Move(move_Point[0],3.5f);
        for (int index = 1; index < people.Count; index++)
        {
            people[index].Move(move_Point[index],1.5f);
        }
    }
}
