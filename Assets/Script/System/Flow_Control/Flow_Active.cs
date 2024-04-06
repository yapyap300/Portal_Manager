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
    private void Active()//kickout�� �����Ѽ� �ٽ� ��Ȱ�� �������� Ȱ��ȭ ��Ű�� ������ �߰� �̺�Ʈ �ߵ�
    {
        Debug.Log($"{GameManager.Instance.time}");
        SoundManager.Instance.PlaySfx("Reactive");
        GameManager.Instance.countPenalty -= GameManager.Instance.kickout.banPenalty;//���� Ű�� onenable���� ���ݱ��� ���� �г�Ƽ�� 0���� �ʱ�ȭ�ؼ� �̸� �Űܵд�.
        GameManager.Instance.isInactive = false;
        GameManager.Instance.kickout.gameObject.SetActive(false);
        GameManager.Instance.portals[GameManager.Instance.InactiveIndex].gameObject.SetActive(true);
        GameManager.Instance.kickout.gameObject.SetActive(true);
    }
}
