using Cinemachine;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flow_MixEvent : Flow_Base
{
    [SerializeField] private GameObject unknown;
    [SerializeField] private Pause_Control pauseControl;
    private bool next = false;
    public override void Enter()
    {
        unknown.SetActive(true);
        PlayEvent();
    }

    public override void Excute(Flow_Control manager)
    {
        if(next)
            manager.SetNextFlow();
    }

    public override void Exit()
    {
        
    }
    private void PlayEvent()
    {
        unknown.GetComponent<Animator>().SetBool("Walk",true);
        unknown.transform.DOMove(new Vector3(0, -4, 0), 5f).OnComplete(() => { unknown.GetComponent<Animator>().SetBool("Walk", false); unknown.SetActive(false);
            next = true; SoundManager.Instance.PlaySfx("Swap"); });
        GameManager.Instance.RandomArea();//다시 뒤섞고 아래에서 포탈들을 다시 초기화
        for(int index = 0; index < GameManager.Instance.maxDestination; index++)//초기화전에 기존 변수값들 저장
        {
            GameManager.Instance.portals[index].EndStage();
            GameManager.Instance.portals[index].Init(GameManager.Instance.portalArea[index]);
        }
        pauseControl.Init();
    }
}
