using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Tutorial_Skip : Tutorial_Base
{
    [SerializeField] Image panel;
    [SerializeField] Button[] yesOrNo;
    bool isNo;
    public override void Enter()
    {
        yesOrNo[0].onClick.AddListener(Yes);
        yesOrNo[1].onClick.AddListener(No);
        panel.gameObject.SetActive(true);
    }

    public override void Excute(Tutorial_Control manager)
    {
        if (isNo)
            manager.SetNextTutorial();
    }

    public override void Exit()
    {
        panel.gameObject.SetActive(false);
    }

    private void Yes()
    {
        SceneManager.LoadScene("Main");
    }
    private void No()
    {
        isNo = true;
    }
}
