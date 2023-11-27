using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial_MaxPeople : Tutorial_Base
{
    [SerializeField] Image[] setting;
    [SerializeField] Image count;// 1�� ���� ǥ�� ���� ui
    [SerializeField] List<People> people;
    [SerializeField] List<Transform> move_Point;
    [SerializeField] AudioSource source;
    [SerializeField] AudioClip sound;
    bool isEnd = false;
    public override void Enter()
    {
        count.fillAmount = 1f;
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

        while (!Input.GetKeyDown(KeyCode.Keypad1))
        {
            yield return null;
        }

        setting[0].gameObject.SetActive(false);
        setting[1].gameObject.SetActive(false);
        source.clip = sound;
        source.Play();
        count.DOColor(Color.black, 0.5f).SetLoops(3).OnComplete(() => count.color = Color.white);
        
        Next();

        isEnd = true;
    }
    private void Next()
    {
        people[0].gameObject.SetActive(false);
        people.RemoveAt(0);
        people[0].Move(move_Point[0], 3.5f);
        for (int index = 1; index < people.Count; index++)
        {
            people[index].Move(move_Point[index], 1.5f);
        }
    }
}
