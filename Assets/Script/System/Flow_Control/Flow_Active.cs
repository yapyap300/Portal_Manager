using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flow_Active : Flow_Base
{
    public override void Enter()
    {
        DOVirtual.DelayedCall(GameManager.Instance.maxTime * 60 / 2, () => Active(), false);
    }

    public override void Excute(Flow_Control manager)
    {
        manager.SetNextFlow();
    }
    public override void Exit()
    {
    }
    private void Active()//kickout을 껏다켜서 다시 비활성 차원문을 활성화 시키는 식으로 중간 이벤트 발동
    {
        Debug.Log($"{GameManager.Instance.time}");
        SoundManager.Instance.PlaySfx("Reactive");
        GameManager.Instance.countPenalty -= GameManager.Instance.kickout.banPenalty;//껏다 키면 onenable에서 지금까지 쌓은 패널티를 0으로 초기화해서 미리 옮겨둔다.
        GameManager.Instance.isInactive = false;
        GameManager.Instance.kickout.gameObject.SetActive(false);
        GameManager.Instance.portals[GameManager.Instance.InactiveIndex].gameObject.SetActive(true);
        GameManager.Instance.kickout.gameObject.SetActive(true);
    }
}
