using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flow_Control : MonoBehaviour //튜토리얼이랑 다른점이 하나라서 상속으로 해결해 볼까 했지만 그러면 변수들의 접근자를 변경해야되서 걍 튜토리얼이랑 별개로 만듬
{
    [SerializeField] private List<Flow_Base> flows;
    private Flow_Base currentBase = null;
    private int currentIndex;
    private void OnEnable()
    {
        GameManager.Instance.Stop();
        currentIndex = -1;        
        SetNextFlow();
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
    }
}
