using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Tutorial_Skip : Tutorial_Base
{
    [SerializeField] Image panel;
    [SerializeField] Button[] yesOrNo;
    bool isNext = false;
    bool isYes = false;
    public override void Enter()
    {
        SoundManager.Instance.SetBGM("Tutorial");
        SoundManager.Instance.PlayBGM();
        yesOrNo[0].onClick.AddListener(Yes);
        yesOrNo[1].onClick.AddListener(No);
        panel.gameObject.SetActive(true);
    }

    public override void Excute(Tutorial_Control manager)
    {
        if (isNext)
            manager.SetNextTutorial(isYes);
    }

    public override void Exit()
    {
        panel.gameObject.SetActive(false);
    }
    //원래는 스킵하면 바로 씬매니저를 통해서 넘겨줬는데 화면이 바로 확 넘어가니까 불편해서 로딩패널을 만들었다. 근데 튜토리얼컨트롤의 흐름 마지막에 로딩패널을 배치했더니 여기서 스킵하면 로딩 패널이
    //안보이는 상황이 발생 그래서 튜토리얼 컨트롤을 고쳐서 스킵이면 바로 로딩인덱스로 점프하게 바꾸었다.
    private void Yes()
    {
        isYes = true;
        isNext = true;
    }
    private void No()
    {
        isNext = true;
    }
}
