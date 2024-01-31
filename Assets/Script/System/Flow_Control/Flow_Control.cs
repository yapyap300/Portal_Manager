using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flow_Control : MonoBehaviour //튜토리얼이랑 다른점이 하나라서 상속으로 해결해 볼까 했지만 그러면 변수들의 접근자를 변경해야되서 걍 튜토리얼이랑 별개로 만듬
{
    [SerializeField] private List<Flow_Base> flows;
    private Flow_Base currentBase = null;
    [SerializeField] private int currentIndex;
    private void OnEnable()
    {
        StartCoroutine(WaitEnd());//다른 이벤트가 실행중이거나 일시정지 상태일때 이벤트를 실행시키면 일시중지가 풀릴때까지 대기했다가 실행하게 해야 중간에 활성화하는게 원활해서 추가
    }
    private void Update()
    {
        if (currentBase != null)
        {
            currentBase.Excute(this);
        }
    }
    public void SetNextFlow()
    {
        if (currentBase != null)
        {
            currentBase.Exit();
        }

        if (currentIndex >= flows.Count - 1)
        {
            EndFlow();
            return;
        }

        currentIndex++;
        currentBase = flows[currentIndex];

        currentBase.Enter();
    }
    public void EndFlow()
    {
        currentBase = null;
        gameObject.SetActive(false);
        GameManager.Instance.Resume();
    }
    IEnumerator WaitEnd()
    {
        yield return new WaitUntil(() => !GameManager.Instance.isStop);
        GameManager.Instance.Stop();
        currentIndex = -1;
        SetNextFlow();
    }
}
