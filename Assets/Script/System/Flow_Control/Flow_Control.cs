using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flow_Control : MonoBehaviour //Ʃ�丮���̶� �ٸ����� �ϳ��� ������� �ذ��� ���� ������ �׷��� �������� �����ڸ� �����ؾߵǼ� �� Ʃ�丮���̶� ������ ����
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
