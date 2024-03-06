using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Flow_UpgradeUI : Flow_Base
{
    [SerializeField] private GameObject winUI;
    [SerializeField] private GameObject upgradeUI;
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] UnityEvent init;
    bool isEnd = false;
    public override void Enter()
    {
        if (GameManager.Instance.endMoney >= 0)
        {
            if (GameManager.Instance.stageIndex == 10)
            {
                SoundManager.Instance.SetBGM("Win");
                SoundManager.Instance.PlayBGM();
                winUI.SetActive(true);
            }
            else
            {
                SoundManager.Instance.SetBGM("Shop");
                SoundManager.Instance.PlayBGM();
                upgradeUI.SetActive(true);
            }
        }
        else
        {
            SoundManager.Instance.SetBGM("GameOver");
            SoundManager.Instance.PlayBGM();
            GameManager.Instance.gameoverCount++;
            GameManager.Instance.stageIndex--;//게임 오버면 다시 시작할때 현 스테이지로 다시 초기화 하기위해 미리 감소
            gameOverUI.SetActive(true);
        }
    }

    public override void Excute(Flow_Control manager)
    {
        if (isEnd)
            manager.SetNextFlow();
    }

    public override void Exit()
    {
        init.Invoke();
        isEnd = false;
    }
    public void Next()
    {
        isEnd = true;
    }
}
