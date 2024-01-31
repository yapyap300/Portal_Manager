using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flow_Control : MonoBehaviour //Ʃ�丮���̶� �ٸ����� �ϳ��� ������� �ذ��� ���� ������ �׷��� �������� �����ڸ� �����ؾߵǼ� �� Ʃ�丮���̶� ������ ����
{
    [SerializeField] private List<Flow_Base> flows;
    private Flow_Base currentBase = null;
    [SerializeField] private int currentIndex;
    private void OnEnable()
    {
        StartCoroutine(WaitEnd());//�ٸ� �̺�Ʈ�� �������̰ų� �Ͻ����� �����϶� �̺�Ʈ�� �����Ű�� �Ͻ������� Ǯ�������� ����ߴٰ� �����ϰ� �ؾ� �߰��� Ȱ��ȭ�ϴ°� ��Ȱ�ؼ� �߰�
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
