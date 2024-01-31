using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : VIP_Base
{
    [SerializeField] Text description;
    private int number;
    protected override void OnEnable()
    {
        base.OnEnable();
        number = Random.Range(1, 6);
        description.text = $"�׻� ������ �е�� ���� �� ({number}��)";
    }
    public override bool NecessaryCondition(Portal portal)
    {
        if (portal.people.Count != GameManager.Instance.maxCount - number)
            return false;
        return true;
    }

    public override void Process()
    {
        GameManager.Instance.countPenalty -= number;
    }
}
