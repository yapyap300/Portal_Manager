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
    //������ ��ŵ�ϸ� �ٷ� ���Ŵ����� ���ؼ� �Ѱ���µ� ȭ���� �ٷ� Ȯ �Ѿ�ϱ� �����ؼ� �ε��г��� �������. �ٵ� Ʃ�丮����Ʈ���� �帧 �������� �ε��г��� ��ġ�ߴ��� ���⼭ ��ŵ�ϸ� �ε� �г���
    //�Ⱥ��̴� ��Ȳ�� �߻� �׷��� Ʃ�丮�� ��Ʈ���� ���ļ� ��ŵ�̸� �ٷ� �ε��ε����� �����ϰ� �ٲپ���.
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
